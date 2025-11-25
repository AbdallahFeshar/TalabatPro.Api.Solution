using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatPro.Api.Core.Entities;
using TalabatPro.Api.Core.Repository.Contract;
using TalabatPro.Api.Core.Specifications.ProductSpecifications;
using TalabatPro.Api.DTOS;
using TalabatPro.Api.Errors;
using TalabatPro.Api.Helpers;

namespace TalabatPro.Api.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetProducts([FromQuery]productSpecParams specParams)
        {
            //var products = await _productRepo.GetAllAsync();
            //return Ok(products);
            var spec = new PoductWithBrandAndCategorySpecification(specParams);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductWithPaginationCount(specParams);
            var totalItems = await _productRepo.GetCountAsync(countSpec);
            var countInPage = data.Count;
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageSize,specParams.PageIndex,totalItems,data,countInPage));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            //var product = await _productRepo.GetByIdAsync(id);

            var spec = new PoductWithBrandAndCategorySpecification(id);
            var product = await _productRepo.GetWithSpecAsync(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));
            var mappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(mappedProduct);
        }
    }

}
