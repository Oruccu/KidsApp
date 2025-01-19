using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Business.Types;
using System.Collections.Generic;
using System.Threading.Tasks;
using KidsAppBackend.Data.Entities; 
using Microsoft.AspNetCore.JsonPatch;

namespace KidsAppBackend.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddChild(AddChildDto dto);
        Task<ResultDto> Login(LoginDto loginDto);
        Task<ResultDto> ParentLogin(LoginDto loginDto);
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task<ResultDto> GetAudioBook(int id);
        Task<ServiceMessage> UpdateUser(int id, UpdateUserDto dto);
        Task<ServiceMessage> DeleteUserAsync(int userId);
        Task<ServiceMessage> DeleteUserById(int id);
        Task<ServiceMessage> SetScore(int id, ScoreDto dto);
        Task<ServiceMessage> AddAnimalSound(CreateAnimalSoundDto dto);
        Task<ServiceMessage> LogoutById(int id);
        Task<ServiceMessage> AddKidsMode(int childId, KidsModeDto dto);
        Task<ServiceMessage> UpdateKidsMode(int childId, KidsModeDto dto);
        Task<ServiceMessage> DeleteKidsMode(int childId);
        Task<KidsModeDto> CreateKidsModeAsync(KidsModeDto kidsModeDto);
        Task<KidsModeDto?> GetKidsModeByIdAsync(int childId);
        Task<KidsModeDto?> UpdateKidsModeAsync(KidsModeDto kidsModeDto);
        Task<ResultDto> GetFavoriteBooksOfChild(int childId);
        Task<ServiceMessage> AddFavoriteBookToChild(int childId, int audioBookId);
         Task<ServiceMessage> PatchUser(int id, JsonPatchDocument<ChildUser> patchDoc);
    }
}
