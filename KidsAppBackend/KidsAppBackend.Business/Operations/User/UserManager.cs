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
        public UserManager(
            IRepository<ChildUser> childRepository,
            IUnitOfWork unitOfWork,
            IRepository<AudioBook> audioBookRepository,
            IConfiguration configuration)
        {
            _childRepository = childRepository;
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = new JwtTokenGenerator(configuration);
            _audioBookRepository = audioBookRepository;
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
                // Hata loglama ekleyebilirsiniz
                return new ResultDto { IsSucced = false, Message = "Bir hata oluştu." };
            }
        }

    }
}
