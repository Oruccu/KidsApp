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
        private readonly IRepository<ParentUser> _parentRepository;
        private readonly IRepository<ChildUser> _childRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IRepository<ParentUser> parentRepository, IRepository<ChildUser> childRepository, IUnitOfWork unitOfWork)
        {
            _parentRepository = parentRepository;
            _childRepository = childRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceMessage> AddParent(AddParentDto dto)
        {
            // E-posta kontrolü
            var existingParent = _parentRepository.Get(p => p.Email == dto.Email);
            if (existingParent != null)
            {
                return new ServiceMessage { IsSucced = false, Message = "A parent with this email already exists." };
            }

            // Şifreyi hash'leme
            var hashedPassword = HashPassword(dto.Password);

            var parent = new ParentUser
            {
                Email = dto.Email,
                Password = hashedPassword
            };

            _parentRepository.Add(parent);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage { IsSucced = true, Message = "Parent successfully added." };
        }

        public async Task<ServiceMessage> AddChild(AddChildDto dto)
        {
            // E-posta kontrolü
            var existingChild = _childRepository.Get(c => c.Email == dto.Email);
            if (existingChild != null)
            {
                return new ServiceMessage { IsSucced = false, Message = "A child with this email already exists." };
            }

            // ParentUserId kontrolü
            var parent = _parentRepository.Get(p => p.Id == dto.ParentUserId);
            if (parent == null)
            {
                return new ServiceMessage { IsSucced = false, Message = "The specified ParentUserId does not exist." };
            }

            // Şifreyi hash'leme
            var hashedPassword = HashPassword(dto.Password);

            var child = new ChildUser
            {
                Email = dto.Email,
                Username = dto.UserName,
                Password = hashedPassword,
                ParentUserId = dto.ParentUserId
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
