using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatPro.Api.Core.Entities;
using TalabatPro.Api.Core.Repository.Contract;

namespace TalabatPro.Api.Controllers
{
   
    public class CategriesContrtoller : BaseApiController
    {
        private readonly IGenericRepository<ProductCategory> _categoryRepo;

        public CategriesContrtoller(IGenericRepository<ProductCategory>categoryRepo)
        {
           _categoryRepo = categoryRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
