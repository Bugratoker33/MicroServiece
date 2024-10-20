using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared.Events;

namespace Order.API.Consumer
{
    public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
    {

        readonly OrderApıDbContext _orderApıDbContext;

        public StockNotReservedEventConsumer(OrderApıDbContext orderApıDbContext)
        {
            _orderApıDbContext = orderApıDbContext;
        }

        //burayı değiştirdik
        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
        {
            Order.API.Models.Entites.Order order = await _orderApıDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == context.Message.OrderId);

            order.OrderStatus = Models.Enums.OrderStatus.Failed;
            await _orderApıDbContext.SaveChangesAsync();
        }

     
    }
}
