using System;
using System.Threading.Tasks;

namespace KidsAppBackend.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackTransactionAsync();
        Task<int> SaveChangesAsync();
    }
}
