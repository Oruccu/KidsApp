using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KidsAppBackend.Data.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity);
        TEntity? GetEntity(int id);
        TEntity? Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity?> GetEntityAsync(int id);
        void Remove(TEntity entity);
    }
}
