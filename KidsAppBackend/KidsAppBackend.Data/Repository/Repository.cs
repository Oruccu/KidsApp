using System;
using System.Collections.Generic;
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
            entity.CreatedAt = DateTime.Now;
            _dbSet.Add(entity);
            //_db.SaveChanges();
        }

        // Soft Delete yapar
        public void Delete(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            entity.IsDeleted = true;
            _dbSet.Update(entity);
            //_db.SaveChanges();
        }

        // ID ile Soft Delete yapar
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        // Belirli bir filtreye göre tek bir kayıt döndürür
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        // Belirli bir filtreye göre tüm kayıtları döndürür
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? _dbSet : _dbSet.Where(predicate);
        }

        // ID'ye göre kayıt döndürür
        public TEntity GetEntity(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
        }

        // Mevcut bir kaydı günceller
        public void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            //_db.SaveChanges();
        }
    }
}

// _db.SaveChanges()'lar transaction durumları göz önüne bulunarak UnitOfWork adı verdiğimiz başka bir pattern içerisinde yönetilecek.