using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities;

namespace TalabatPro.Api.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        /* return(IReadOnlyList<T>) await _dbContext.Set<Product>()
                    .Include(p=>p.Brand)
                    .Include(p=>p.Category)
                    .ToListAsync();*/
        public Expression<Func<T,bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Inculdes { get; set; }
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public Expression<Func<T, object>>? OrderByDescending { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

    }
}
