using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatPro.Api.Core.Specifications.ProductSpecifications
{
    public class productSpecParams
    {
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? SearchProductName { get; set; }
        private int pageIndex = 1;
        public int PageIndex
        {
            get => pageIndex;
            set => pageIndex = (value < 1) ? 1 : value;
        }
        const int maxPageSize = 10;
        private int pageSize=5;
        public int PageSize 
        { get => pageSize;
            set 
            {
                pageSize = value> maxPageSize ? maxPageSize : value;
            }
        }

    }
}
