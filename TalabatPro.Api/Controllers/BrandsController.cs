using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatPro.Api.Core.Entities;
using TalabatPro.Api.Core.Repository.Contract;

namespace TalabatPro.Api.Controllers
{
   
    public class BrandsController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public BrandsController(IGenericRepository<ProductBrand> brandRepo)
        {
            _brandRepo = brandRepo;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }
    }
}
