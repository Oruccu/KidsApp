using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace KidsAppBackend.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KidsAppDbContext _db;
        private IDbContextTransaction _transaction;

        public UnitOfWork(KidsAppDbContext db){
            _db = db;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
            // Garbage Colletor'a sen bunu temizleyebilirsin iznini verdiğimiz yer.
            // O an silmiyor - Silinebilir yapıyoruz.
            //GC.Collect();
            // GC.WaitForPendingFinalizers();
            // Bu kodlar Garbage Collector'ı direk çalıştırır.
        }

        public async Task RollBackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}