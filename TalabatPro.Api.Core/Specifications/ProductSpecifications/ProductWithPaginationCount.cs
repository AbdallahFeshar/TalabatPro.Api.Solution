using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities;

namespace TalabatPro.Api.Core.Specifications.ProductSpecifications
{
    public class ProductWithPaginationCount : Specification<Product>
    {
        public ProductWithPaginationCount(productSpecParams specParams) : base(p =>

              (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value)
            &&
              (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value))
        {
            Inculdes.Add(p => p.Brand);
            Inculdes.Add(p => p.Category);
     
        }
    }
}
