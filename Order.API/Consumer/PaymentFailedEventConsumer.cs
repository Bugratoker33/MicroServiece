using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared.Events;

namespace Order.API.Consumer
{
    public class PaymentFailedEventConsumer:IConsumer<PaymentFaildEvent>
    {
        readonly OrderApıDbContext _orderApıDbContext;

        public PaymentFailedEventConsumer(OrderApıDbContext orderApıDbContext)
        {
            _orderApıDbContext = orderApıDbContext;
        }

        public async Task Consume(ConsumeContext<PaymentFaildEvent> context)
        {
            Order.API.Models.Entites.Order order = await _orderApıDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == context.Message.OrderId);

            order.OrderStatus = Models.Enums.OrderStatus.Failed;
            await _orderApıDbContext.SaveChangesAsync();
        }
    }
}
