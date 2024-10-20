using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared.Events;

namespace Order.API.Consumer
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {

        readonly OrderApıDbContext _orderApıDbContext;

        public PaymentCompletedEventConsumer(OrderApıDbContext orderApıDbContext)
        {
            _orderApıDbContext = orderApıDbContext;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
           Order.API.Models.Entites.Order order = await _orderApıDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == context.Message.OrderId);

            order.OrderStatus = Models.Enums.OrderStatus.Completed;
            await _orderApıDbContext.SaveChangesAsync();
        }
    }
}
