using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB_API.Context;
using IMDB_API.Models;

namespace IMDB_API.Repositories
{
    public abstract class EFCoreRepository<TEntity, TContext> : IRepository<TEntity>
    {
        private readonly TContext _context;

        public EFCoreRepository(TContext context)
        {
            _context = context;
        }

        public Task<List<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IRepository<TEntity>.Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
