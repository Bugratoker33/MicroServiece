using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.ViewModels;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        readonly OrderApıDbContext _context;

        public OrdersController(OrderApıDbContext context)
        {
            _context = context;
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
            return Ok();
        }
    }
}
