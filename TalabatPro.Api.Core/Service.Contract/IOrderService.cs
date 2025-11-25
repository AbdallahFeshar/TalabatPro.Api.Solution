using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities.OrderModule;

namespace TalabatPro.Api.Core.Service.Contract
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrderForUserAsync();
        Task<Order>GetOrderByIdForUserAsync();


    }
}
