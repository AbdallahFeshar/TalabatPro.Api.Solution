using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities.BasketModule;

namespace TalabatPro.Api.Core.Repository.Contract
{
    public interface IBasketRepository
    {
        Task<CustomerBasket>GetBasketAsync(string basketId);
        Task<CustomerBasket>UpdateOrCreateBasketAsync(CustomerBasket basket);
        Task<bool>DeleteBasketAsync(string basketId);
    }
}
