using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities;

namespace TalabatPro.Api.Core.Specifications.ProductSpecifications
{
    public class PoductWithBrandAndCategorySpecification:Specification<Product>
    {
        public PoductWithBrandAndCategorySpecification(productSpecParams specParams):base(p=>
            
              (!specParams.BrandId.HasValue|| p.BrandId == specParams.BrandId.Value)
            &&
              (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)
            &&(string.IsNullOrEmpty(specParams.SearchProductName)||(p.Name.ToLower().Contains(specParams.SearchProductName.ToLower()))

        ))
        {
            Inculdes.Add(p => p.Brand);
            Inculdes.Add(p => p.Category);
            //switch (specParams.Sort)
            //{
            //    case "priceAsc":
            //        OrderBy = p => p.Price;
            //        break;
            //    case "priceDesc":
            //        OrderByDescending = p => p.Price;
            //        break;
            //    default:
            //        OrderBy = p => p.Name;
            //        break;
            //}

            int Take = specParams.PageSize;
            int Skip = specParams.PageSize * ((specParams.PageIndex ) - 1);
            ApplyPagination(Take, Skip);


        }
        public PoductWithBrandAndCategorySpecification(int id)
            :base(p=>p.Id==id)
        {
            Inculdes.Add(p => p.Brand);
            Inculdes.Add(p => p.Category);
        }
    }
}
