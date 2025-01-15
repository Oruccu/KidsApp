using KidsAppBackend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KidsAppBackend.Data.Repositories
{
    public class KidsModeRepository : IKidsModeRepository
    {
        private readonly KidsAppDbContext _dbContext;

        public KidsModeRepository(KidsAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<KidsMode?> GetKidsModeByChildIdAsync(int childId)
        {
            return await _dbContext.KidsModes.FirstOrDefaultAsync(k => k.ChildId == childId && !k.IsDeleted);
        }

        public async Task UpdateKidsModeAsync(int childId, bool isBoy, bool isGirl)
        {
            var kidsMode = await GetKidsModeByChildIdAsync(childId);
            if (kidsMode == null) throw new KeyNotFoundException($"No KidsMode found for ChildId {childId}.");

            kidsMode.Boy = isBoy;
            kidsMode.Girl = isGirl;
            kidsMode.UpdatedAt = DateTime.Now;

            _dbContext.KidsModes.Update(kidsMode);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(KidsMode kidsMode) // Yeni metod
        {
            await _dbContext.KidsModes.AddAsync(kidsMode);
            await _dbContext.SaveChangesAsync();
        }
    }
}
