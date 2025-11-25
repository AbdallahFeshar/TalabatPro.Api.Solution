using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities.BasketModule;
using TalabatPro.Api.Core.Repository.Contract;

namespace TalabatPro.Api.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer database) 
        {
            _database = database.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var item=await _database.StringGetAsync(basketId);
            if (!item.IsNullOrEmpty)
                return JsonSerializer.Deserialize<CustomerBasket>(item);
            return null;
        }
        public async Task<CustomerBasket> UpdateOrCreateBasketAsync(CustomerBasket basket)
        {
           var BasketItem= await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(2));
            if(!BasketItem)
                return null;
            return await GetBasketAsync(basket.Id);

        }
    }
}
