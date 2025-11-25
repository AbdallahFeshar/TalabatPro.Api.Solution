using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities;
using TalabatPro.Api.Core.Repository.Contract;
using TalabatPro.Api.Repository.Data;

namespace TalabatPro.Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext dbContext) 
        {
            _repositories = new Hashtable();
            _dbContext = dbContext;
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
         public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null)
                _repositories = new Hashtable();
            var key=typeof(TEntity).Name;
            if(!_repositories.ContainsKey(key))
            {
                var repository=new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(key,repository);
            }
            return _repositories[key] as IGenericRepository<TEntity>;
        }
    }
}
