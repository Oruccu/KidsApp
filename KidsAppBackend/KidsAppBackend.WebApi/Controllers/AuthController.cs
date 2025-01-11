using Microsoft.AspNetCore.Mvc;
using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Business.Operations.User;
using System.Threading.Tasks;
using System.Linq;
using KidsAppBackend.WebApi.Models;

namespace KidsAppBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Ebeveyn Kayıt Endpoint'i
        [HttpPost("registerParent")]
        public async Task<IActionResult> RegisterParent([FromBody] ParentRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var addParentDto = new AddParentDto
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _userService.AddParent(addParentDto);
            if (!result.IsSucced)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { IsSucced = result.IsSucced, Message = result.Message });
        }

        // Çocuk Kayıt Endpoint'i
        [HttpPost("registerChild")]
        public async Task<IActionResult> RegisterChild([FromBody] ChildRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var addChildDto = new AddChildDto
            {
                Email = request.Email,
                Password = request.Password,
                UserName = request.UserName,
                ParentUserId = request.ParentUserId
            };

            var result = await _userService.AddChild(addChildDto);
            if (!result.IsSucced)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { IsSucced = result.IsSucced, Message = result.Message });
        }
    }
}
