using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KidsAppBackend.Data.Entities;

namespace KidsAppBackend.Data.Repositories
{
public interface IKidsModeRepository
{
    Task CreateAsync(KidsMode kidsMode);
    Task<KidsMode?> GetKidsModeByChildIdAsync(int childId);
    Task UpdateKidsModeAsync(int childId, bool isBoy, bool isGirl);
}
}