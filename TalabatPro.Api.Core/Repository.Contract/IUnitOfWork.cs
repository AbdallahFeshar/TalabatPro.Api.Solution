using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities;

namespace TalabatPro.Api.Core.Repository.Contract
{
    public interface IUnitOfWork
    {
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
    }
}
