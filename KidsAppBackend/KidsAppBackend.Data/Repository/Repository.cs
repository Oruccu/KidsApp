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

        // Yeni bir kayıt ekler
        public void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.CreatedAt = DateTime.Now;
            _dbSet.Add(entity);
        }

        // Soft Delete yapar
        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        // ID ile Soft Delete yapar
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            Delete(entity);
        }

        // Belirli bir filtreye göre tek bir kayıt döndürür
        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        // Belirli bir filtreye göre tüm kayıtları döndürür
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbSet.Where(e => !e.IsDeleted);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }

        // ID'ye göre kayıt döndürür
        public TEntity? GetEntity(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
        }

        // Mevcut bir kaydı günceller
        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.UpdatedAt = DateTime.Now;
            _dbSet.Update(entity);
        }
    }
}
