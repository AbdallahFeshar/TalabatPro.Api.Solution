using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities;
using TalabatPro.Api.Core.Specifications;

namespace TalabatPro.Api.Repository
{
    public static class SpecificationEvaluator<T>where T:BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);
            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Inculdes.Aggregate(query, (q1, q2) => q1.Include(q2));


            return query;

        }
    }
}
