using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities;
using TalabatPro.Api.Core.Specifications;

namespace TalabatPro.Api.Core.Repository.Contract
{
    public interface IGenericRepository<T>where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?>GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T>spec);
        Task<T?> GetWithSpecAsync(ISpecification<T> spec);
    }
}
