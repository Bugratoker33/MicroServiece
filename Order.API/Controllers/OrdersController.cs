using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.ViewModels;
using Shared.Events;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        readonly OrderApıDbContext _context;
        readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(OrderApıDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderVM creatOrder)
        {
            Order.API.Models.Entites.Order order = new()
            {
                OrderId = Guid.NewGuid(),
                BuyerId= creatOrder.BuyerId,
                CreatedDate = DateTime.Now,
                OrderStatus= Models.Enums.OrderStatus.Suspend
            };

            order.OrderItems= creatOrder.OrderItems.Select(orderItems => new Models.Entites.OrderItem
            {
                Count = orderItems.Count, 
                Price = orderItems.Price,
                ProductId   = orderItems.ProductId,
                
            }).ToList();

            order.TotalPrice = creatOrder.OrderItems.Sum(orderItems => (orderItems.Price*orderItems.Count));
         await   _context.Orders.AddAsync(order);
          await   _context.SaveChangesAsync();

            OrderCreatedEvent orderCreatedEvent = new()
            {
                BuyerId = order.BuyerId,
                OrderId = order.OrderId,
                OrderItems = order.OrderItems.Select(orderItems => new Shared.Messages.OrderItemMessage
                {
                    Count = orderItems.Count,
                    ProductId= orderItems.ProductId,

                }).ToList()
            };
            _publishEndpoint.Publish(orderCreatedEvent);

            return Ok();
        }
    }
}
