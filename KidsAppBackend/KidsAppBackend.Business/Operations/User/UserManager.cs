using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Business.Types;
using KidsAppBackend.Data.Entities;
using KidsAppBackend.Data.Repositories;
using KidsAppBackend.Data.UnitOfWork;
using KidsAppBackend.Business.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidsAppBackend.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IRepository<ChildUser> _childRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IRepository<AudioBook> _audioBookRepository;
        private readonly IRepository<ChildUserAudioBook> _childUserAudioBookRepository;
        private readonly IKidsModeRepository _kidsModeRepository;
        private readonly ITokenBlacklist _tokenBlacklist;

        public UserManager(
            IRepository<ChildUser> childRepository,
            IUnitOfWork unitOfWork,
            IRepository<AudioBook> audioBookRepository,
            IRepository<ChildUserAudioBook> childUserAudioBookRepository,
            IKidsModeRepository kidsModeRepository,
            ITokenBlacklist tokenBlacklist,
            IConfiguration configuration)
        {
            _childRepository = childRepository;
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = new JwtTokenGenerator(configuration);
            _childUserAudioBookRepository = childUserAudioBookRepository;
            _audioBookRepository = audioBookRepository;
            _kidsModeRepository = kidsModeRepository;
            _tokenBlacklist = tokenBlacklist;
        }

        public async Task<ServiceMessage> AddChild(AddChildDto dto)
        {
            var existingChild = _childRepository.Get(c => c.Email == dto.Email);
            if (existingChild != null) return new ServiceMessage { IsSucced = false, Message = "A child with this email already exists." };
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var child = new ChildUser { Email = dto.Email, Username = dto.UserName, Password = hashedPassword, ParentUserName = dto.ParentUserName };
            _childRepository.Add(child);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceMessage { IsSucced = true, Message = "Child successfully added!" };
        }

        public async Task<ResultDto> Login(LoginDto loginDto)
        {
            var user = _childRepository.Get(c => c.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return new ResultDto { IsSucced = false, Message = "Invalid username or password." };

            var existingMode = await _kidsModeRepository.GetKidsModeByChildIdAsync(user.Id);
            if (existingMode == null)
            {
                var defaultMode = new KidsMode { ChildId = user.Id, Mode = ModeType.Girl, UpdatedAt = DateTime.UtcNow };
                await _kidsModeRepository.CreateAsync(defaultMode);
                await _unitOfWork.SaveChangesAsync();
            }
            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Username);
            return new ResultDto { IsSucced = true, Token = token, Message = "Login successful.", ChildId = user.Id, UserName = user.Username };
        }


        public async Task<ResultDto> ParentLogin(LoginDto loginDto)
        {
            var parent = _childRepository.Get(p => p.ParentUserName == loginDto.Email);
            if (parent == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, parent.Password))
                return new ResultDto { IsSucced = false, Message = "Invalid parent credentials." };
            var token = _jwtTokenGenerator.GenerateToken(parent.Id, parent.Email, parent.Username);
            return new ResultDto { IsSucced = true, Token = token, Message = "Parent login successful.", UserName = parent.Username };
        }

        public async Task<ServiceMessage> AddFavoriteBookToChild(int childId, int audioBookId)
        {
            var child = await _childRepository.GetAsync(x => x.Id == childId);
            if (child == null)
            {
                return new ServiceMessage { IsSucced = false, Message = "Child not found." };
            }

            var audioBook = await _audioBookRepository.GetAsync(x => x.Id == audioBookId);
            if (audioBook == null)
            {
                return new ServiceMessage { IsSucced = false, Message = "AudioBook not found." };
            }

            var childUserAudioBook = new ChildUserAudioBook
            {
                ChildUserId = childId,
                AudioBookId = audioBookId
            };

            _childUserAudioBookRepository.Add(childUserAudioBook);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage { IsSucced = true, Message = "Favorite book added successfully." };
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = _childRepository.GetAll().Select(x => new UserDto { Id = x.Id, Email = x.Email, UserName = x.Username }).ToList();
            return users;
        }
        public async Task<ResultDto> GetFavoriteBooksOfChild(int childId)
        {
            var child = await _childRepository.GetAsync(x => x.Id == childId);
            if (child == null)
            {
                return new ResultDto { IsSucced = false, Message = "Child not found." };
            }
            var favoriteBookIds = _childUserAudioBookRepository
                                  .GetAll(x => x.ChildUserId == childId)
                                  .Select(x => x.AudioBookId);

            var favoriteBooks = _audioBookRepository.GetAll(b => favoriteBookIds.Contains(b.Id))
                                    .Select(b => new AudioBookDto
                                    {
                                        Title = b.Title,
                                        AudioFileUrl = b.AudioFileUrl
                                    })
                                    .ToList();

            return new ResultDto { IsSucced = true, Message = "Favorites fetched.", Data = favoriteBooks };
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = _childRepository.Get(x => x.Id == id);
            if (user == null) return null;
            return new UserDto { Id = user.Id, Email = user.Email, UserName = user.Username };
        }

        public async Task<ResultDto> GetAudioBook(int id)
        {
            var audioBook = await _audioBookRepository.GetEntityAsync(id);
            if (audioBook == null) return new ResultDto { IsSucced = false, Message = "AudioBook bulunamadı." };
            var dto = new AudioBookDto { Title = audioBook.Title, AudioFileUrl = audioBook.AudioFileUrl };
            return new ResultDto { IsSucced = true, Message = "AudioBook başarıyla alındı.", Data = dto };
        }

        public async Task<ServiceMessage> UpdateUser(int id, UpdateUserDto dto)
        {
            var user = _childRepository.Get(x => x.Id == id);
            if (user == null) return new ServiceMessage { IsSucced = false, Message = "User not found." };
            user.Username = dto.UserName;
            user.Email = dto.Email;
            _childRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceMessage { IsSucced = true, Message = "User updated." };
        }

        public async Task<ServiceMessage> DeleteUserAsync(int userId)
        {
            var existingChild = await _childRepository.GetAsync(c => c.Id == userId);
            if (existingChild == null) return new ServiceMessage { IsSucced = false, Message = "User not found." };
            _childRepository.Remove(existingChild);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceMessage { IsSucced = true, Message = "User deleted successfully." };
        }

        public async Task<ServiceMessage> DeleteUserById(int id)
        {
            var user = _childRepository.Get(x => x.Id == id);
            if (user == null) return new ServiceMessage { IsSucced = false, Message = "User not found." };
            _childRepository.Remove(user);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceMessage { IsSucced = true, Message = "User deleted." };
        }

        public async Task<ServiceMessage> SetScore(int id, ScoreDto dto)
        {
            var user = _childRepository.Get(x => x.Id == id);
            if (user == null) return new ServiceMessage { IsSucced = false, Message = "User not found." };
            user.Score = dto.Score;
            _childRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceMessage { IsSucced = true, Message = "Score set." };
        }

        public async Task<ServiceMessage> AddAnimalSound(CreateAnimalSoundDto dto)
        {
            return new ServiceMessage { IsSucced = true, Message = "Animal sound added." };
        }

        public async Task<ServiceMessage> LogoutById(int id)
        {
            return new ServiceMessage { IsSucced = true, Message = "User logged out by id." };
        }

        public async Task<ServiceMessage> AddKidsMode(int childId, KidsModeDto dto)
        {
            return new ServiceMessage { IsSucced = true, Message = "Kids mode added." };
        }

        public async Task<ServiceMessage> UpdateKidsMode(int childId, KidsModeDto dto)
        {
            return new ServiceMessage { IsSucced = true, Message = "Kids mode updated." };
        }

        public async Task<ServiceMessage> DeleteKidsMode(int childId)
        {
            return new ServiceMessage { IsSucced = true, Message = "Kids mode deleted." };
        }

        public async Task<KidsModeDto> CreateKidsModeAsync(KidsModeDto kidsModeDto)
        {
            if (!Enum.TryParse<ModeType>(kidsModeDto.Mode, true, out var mode)) throw new ArgumentException("Invalid mode value.");
            var kidsMode = new KidsMode { ChildId = kidsModeDto.ChildId, Mode = mode, UpdatedAt = DateTime.Now };
            await _kidsModeRepository.CreateAsync(kidsMode);
            await _unitOfWork.SaveChangesAsync();
            return kidsModeDto;
        }

        public async Task<KidsModeDto?> GetKidsModeByIdAsync(int childId)
        {
            var kidsMode = await _kidsModeRepository.GetKidsModeByChildIdAsync(childId);
            if (kidsMode == null) return null;
            return new KidsModeDto { ChildId = kidsMode.ChildId, Mode = kidsMode.Mode.ToString() };
        }

        public async Task<KidsModeDto?> UpdateKidsModeAsync(KidsModeDto kidsModeDto)
        {
            if (!Enum.TryParse<ModeType>(kidsModeDto.Mode, true, out var mode)) throw new ArgumentException("Invalid mode value.");
            await _kidsModeRepository.UpdateKidsModeAsync(kidsModeDto.ChildId, mode);
            return kidsModeDto;
        }

        public async Task<ServiceMessage> PatchUser(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<ChildUser> patchDoc)
        {
            var user = await _childRepository.GetEntityAsync(id);
            if (user == null)
            {
                return new ServiceMessage { IsSucced = false, Message = "User not found." };
            }

            patchDoc.ApplyTo(user);

            _childRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage { IsSucced = true, Message = "User patched successfully." };
        }
    }
}
