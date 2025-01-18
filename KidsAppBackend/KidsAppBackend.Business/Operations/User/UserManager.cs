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
        private readonly ITokenBlacklist _tokenBlacklist;
        public UserManager(
            IRepository<ChildUser> childRepository,
            IUnitOfWork unitOfWork,
            IRepository<AudioBook> audioBookRepository,
            IKidsModeRepository kidsModeRepository,
            ITokenBlacklist tokenBlacklist,
            IConfiguration configuration)
        {
            _childRepository = childRepository;
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = new JwtTokenGenerator(configuration);
            _audioBookRepository = audioBookRepository;
            _kidsModeRepository = kidsModeRepository;
            _tokenBlacklist = tokenBlacklist;
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

        public async Task<ResultDto> ParentLogin(LoginDto loginDto)
        {
            var parent = _childRepository.Get(p => p.ParentUserName == loginDto.Email);
            if (parent == null || !VerifyPassword(parent.Password, loginDto.Password))
            {
                return new ResultDto { IsSucced = false, Message = "Invalid parent credentials." };
            }

            var token = _jwtTokenGenerator.GenerateToken(parent.Id, parent.Email, parent.Username);
            return new ResultDto
            {
                IsSucced = true,
                Token = token,
                Message = "Parent login successful.",
                UserName = parent.Username
            };
        }

        public async Task<ResultDto> Login(LoginDto loginDto)
        {
            try 
            {
                var user = _childRepository.Get(c => c.Email == loginDto.Email);
                if (user == null || !VerifyPassword(user.Password, loginDto.Password))
                {
                    return new ResultDto { IsSucced = false, Message = "Invalid username or password." };
                }

                // Kullanıcı girişi başarılı, varsayılan mode'u kontrol et
                var existingMode = await _kidsModeRepository.GetKidsModeByChildIdAsync(user.Id);
                if (existingMode == null)
                {
                    // Varsayılan mode yok, oluştur
                    var defaultMode = new KidsMode
                    {
                        ChildId = user.Id,
                        Mode = ModeType.Girl,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await _kidsModeRepository.CreateAsync(defaultMode);
                    await _unitOfWork.SaveChangesAsync();
                }

                // Token oluştur
                var token = _jwtTokenGenerator.GenerateToken(
                    userId: user.Id,
                    email: user.Email,
                    userName: user.Username
                );

                return new ResultDto
                {
                    IsSucced = true,
                    Token = token,
                    Message = "Login successful.",
                    ChildId = user.Id,
                    UserName = user.Username
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return new ResultDto 
                { 
                    IsSucced = false, 
                    Message = "An error occurred during login." 
                };
            }
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
                if (!Enum.TryParse<ModeType>(kidsModeDto.Mode, true, out var mode))
                {
                    throw new ArgumentException("Invalid mode value. Allowed values are 'Boy' or 'Girl'.");
                }

                var kidsMode = new KidsMode
                {
                    ChildId = kidsModeDto.ChildId,
                    Mode = mode,
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
                    Mode = kidsMode.Mode.ToString()
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
                if (!Enum.TryParse<ModeType>(kidsModeDto.Mode, true, out var mode))
                {
                    throw new ArgumentException("Invalid mode value. Allowed values are 'Boy' or 'Girl'.");
                }

                await _kidsModeRepository.UpdateKidsModeAsync(kidsModeDto.ChildId, mode);
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
