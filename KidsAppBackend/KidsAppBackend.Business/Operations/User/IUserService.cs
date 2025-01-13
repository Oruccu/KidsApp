using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Business.Types;

namespace KidsAppBackend.Business.Operations.User
{
    public interface IUserService {

        Task<ServiceMessage> AddChild(AddChildDto dto);
        Task<ResultDto> Login(LoginDto loginDto);
    }
}