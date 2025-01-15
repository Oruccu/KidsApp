using KidsAppBackend.Data.Entities;
using KidsAppBackend.Data.Enums;
using System.Threading.Tasks;

namespace KidsAppBackend.Data.Repositories
{
    public interface IKidsModeRepository
    {
        Task CreateAsync(KidsMode kidsMode);
        Task<KidsMode?> GetKidsModeByChildIdAsync(int childId);
        Task UpdateKidsModeAsync(int childId, ModeType mode); // Güncellenmiş imza
    }
}
