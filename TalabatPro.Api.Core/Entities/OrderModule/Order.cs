using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatPro.Api.Core.Entities.OrderModule
{
    public class Order:BaseEntity
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public Address ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal SubTotal { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Cost;
        }
        public string PaymentIntentId { get; set; }
        public Order()
        {

        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal/*, string paymentIntentId*/)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            //PaymentIntentId = paymentIntentId;
        }
    }
}
