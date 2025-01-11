using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KidsAppBackend.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        //Kaç kayda etki ettiğini geriye döner, o yüzden int kullanıldı.

        Task BeginTransaction();
        //Taks -> Asenkron metotların voidi gibi düşünülebilir.

        Task CommitTransaction();

        Task RollBackTransaction();
    }
}