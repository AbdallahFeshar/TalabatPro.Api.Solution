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
        public PoductWithBrandAndCategorySpecification(string sort, int? brandid, int? categoryid):base(p=>
            
              (!brandid.HasValue|| p.BrandId == brandid.Value)
            &&
              (!categoryid.HasValue || p.CategoryId == categoryid.Value)

        )
        {
            Inculdes.Add(p => p.Brand);
            Inculdes.Add(p => p.Category);
            switch (sort)
            {
                case "priceAsc":
                    OrderBy = p => p.Price;
                    break;
                case "priceDesc":
                    OrderByDescending = p => p.Price;
                    break;
                default:
                    OrderBy = p => p.Name;
                    break;
            }
        }
        public PoductWithBrandAndCategorySpecification(int id)
            :base(p=>p.Id==id)
        {
            Inculdes.Add(p => p.Brand);
            Inculdes.Add(p => p.Category);
        }
    }
}
