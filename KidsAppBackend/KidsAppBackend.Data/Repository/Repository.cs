using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KidsAppBackend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KidsAppBackend.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly KidsAppDbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(KidsAppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.CreatedAt = DateTime.Now;
            _dbSet.Add(entity);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            Delete(entity);
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbSet.Where(e => !e.IsDeleted);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }

        public TEntity? GetEntity(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.UpdatedAt = DateTime.Now;
            _dbSet.Update(entity);
        }
        public async Task<TEntity?> GetEntityAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
