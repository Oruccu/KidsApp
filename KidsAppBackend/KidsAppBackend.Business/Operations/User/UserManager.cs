using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Data.Repositories;
using KidsAppBackend.Data.UnitOfWork;
using KidsAppBackend.Data.Entities;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Collections.Generic;
using KidsAppBackend.Business.Types;

namespace KidsAppBackend.Business.Operations.User
{
    public class UserManager : IUserService
    {

        private readonly IRepository<ChildUser> _childRepository;
        private readonly IUnitOfWork _unitOfWork;
    
        public UserManager( IRepository<ChildUser> childRepository, IUnitOfWork unitOfWork)
        {
            _childRepository = childRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceMessage> AddChild(AddChildDto dto)
        {
            // E-posta kontrolü
            var existingChild = _childRepository.Get(c => c.Email == dto.Email);
            if (existingChild != null)
            {
                return new ServiceMessage { IsSucced = false, Message = "A child with this email already exists." };
            }

            // Şifreyi hash'leme
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

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
