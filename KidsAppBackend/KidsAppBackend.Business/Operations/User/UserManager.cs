using KidsAppBackend.Business.Operations.User;
using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Data.Repositories;
using KidsAppBackend.Data.UnitOfWork;
using KidsAppBackend.Data.Entities;
using KidsAppBackend.Business.Types;
using KidsAppBackend.Business.Utilities;
using Microsoft.Extensions.Configuration;

namespace KidsAppBackend.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IRepository<ChildUser> _childRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IRepository<AudioBook> _audioBookRepository;
        private readonly IKidsModeRepository _kidsModeRepository;
        public UserManager(
            IRepository<ChildUser> childRepository,
            IUnitOfWork unitOfWork,
            IRepository<AudioBook> audioBookRepository,
            IKidsModeRepository kidsModeRepository,
            IConfiguration configuration)
        {
            _childRepository = childRepository;
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = new JwtTokenGenerator(configuration);
            _audioBookRepository = audioBookRepository;
            _kidsModeRepository = kidsModeRepository;
        }

        public async Task<ServiceMessage> AddChild(AddChildDto dto)
        {
            var existingChild = _childRepository.Get(c => c.Email == dto.Email);
            if (existingChild != null)
            {
                return new ServiceMessage { IsSucced = false, Message = "A child with this email already exists." };
            }

            var hashedPassword = HashPassword(dto.Password);

            var child = new ChildUser
            {
                Email = dto.Email,
                Username = dto.UserName,
                Password = hashedPassword,
                ParentUserName = dto.ParentUserName,
            };

            _childRepository.Add(child);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage { IsSucced = true, Message = "Child successfully added!" };
        }

        public async Task<ResultDto> Login(LoginDto loginDto)
        {
            var user = _childRepository.Get(c => c.Email == loginDto.Email);
            if (user == null || !VerifyPassword(user.Password, loginDto.Password))
            {
                return new ResultDto { IsSucced = false, Message = "Invalid username or password." };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new ResultDto { IsSucced = true, Token = token, Message = "Login successful." };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string hashedPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<ResultDto> GetAudioBook(int id)
        {
            try
            {
                var audioBook = await _audioBookRepository.GetEntityAsync(id);
                if (audioBook == null)
                {
                    return new ResultDto { IsSucced = false, Message = "AudioBook bulunamadı." };
                }

                var audioBookDto = new AudioBookDto
                {
                    Title = audioBook.Title,
                    AudioFileUrl = audioBook.AudioFileUrl
                };

                return new ResultDto
                {
                    IsSucced = true,
                    Message = "AudioBook başarıyla alındı.",
                    Data = audioBookDto
                };
            }
            catch (Exception ex)
            {
                return new ResultDto { IsSucced = false, Message = "Bir hata oluştu." };
            }
        }
        public async Task<KidsModeDto> CreateKidsModeAsync(KidsModeDto kidsModeDto)
        {
            try
            {
                var kidsMode = new KidsMode
                {
                    ChildId = kidsModeDto.ChildId,
                    Boy = kidsModeDto.Boy,
                    Girl = kidsModeDto.Girl,
                    UpdatedAt = DateTime.Now
                };

                await _kidsModeRepository.CreateAsync(kidsMode);
                await _unitOfWork.SaveChangesAsync();

                return kidsModeDto;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating KidsMode.", ex);
            }
        }
        public async Task<KidsModeDto?> GetKidsModeByIdAsync(int childId)
        {
            try
            {
                var kidsMode = await _kidsModeRepository.GetKidsModeByChildIdAsync(childId);
                if (kidsMode == null) return null;

                return new KidsModeDto
                {
                    ChildId = kidsMode.ChildId,
                    Boy = kidsMode.Boy,
                    Girl = kidsMode.Girl
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching KidsMode.", ex);
            }
        }
        public async Task<KidsModeDto?> UpdateKidsModeAsync(KidsModeDto kidsModeDto)
        {
            try
            {
                await _kidsModeRepository.UpdateKidsModeAsync(kidsModeDto.ChildId, kidsModeDto.Boy, kidsModeDto.Girl);
                return kidsModeDto;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating KidsMode.", ex);
            }
        }

    }
}
