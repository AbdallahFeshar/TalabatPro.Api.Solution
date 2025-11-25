using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatPro.Api.Core.Entities.BasketModule;
using TalabatPro.Api.Core.Repository.Contract;

namespace TalabatPro.Api.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;

        public BasketController(IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
        }
        [HttpGet("{basketId}")]
        public async Task<ActionResult> GetBasket(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if(basket==null)
                return NotFound(new {message="Basket not found"});
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateBasket(CustomerBasket basket)
        {
            var UpdateBasket=await _basketRepo.UpdateOrCreateBasketAsync(basket);
            if(UpdateBasket==null)
                return BadRequest(new {message="Problem in updating the basket"});
            return Ok(UpdateBasket);
        }

        [HttpDelete("{basketId}")]
        public async Task<ActionResult<bool>>DeleteBasket(string basketId)
        {
            var item= await _basketRepo.DeleteBasketAsync(basketId);
            if(!item)
                return BadRequest(new {message="Problem in deleting the basket"});
            return Ok(item);
        }
    }
}
