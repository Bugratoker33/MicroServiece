using Microsoft.AspNetCore.Identity;
using Order.API.Models.Enums;

namespace Order.API.Models.Entites
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public Guid BuyerId { get; set; }

        public Decimal  TotalPrice { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public OrderStatus  OrderStatus { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
