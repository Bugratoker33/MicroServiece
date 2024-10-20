using MassTransit;
using Shared.Events;
using System.Net.WebSockets;

namespace Payment.API.Consumer
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Consume(ConsumeContext<StockReservedEvent> context)
        {


            if (true)
            {
                PaymentCompletedEvent paymentCompletedEvent = new()
                {
                    OrderId = context.Message.OrderId,
                };

                _publishEndpoint.Publish(paymentCompletedEvent);
                Console.WriteLine("Ödeme başarılı ");
                Console.WriteLine("Ödeme başarılı ");
            }
            else
            {
                PaymentFaildEvent paymentFaildEvent = new()
                {
                    OrderId = context.Message.OrderId,
                    Message = "bakiyeniz yettersiz"
                };
                Console.WriteLine("Ödeme başarısız");
            }






            return Task.CompletedTask;
        }
    }
}
