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
                ParentUserName = request.ParentUserName,
            };

            var result = await _userService.AddChild(addChildDto);
            if (!result.IsSucced)
            {
                Console.WriteLine(result.Message);
                return BadRequest(result.Message);
            }
            Console.WriteLine($"IsSucced: {result.IsSucced}, Message: {result.Message}");

            return Ok(
                new
                {
                    IsSucced = result.IsSucced,
                    Message = result.Message
                });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var loginDto = new LoginDto
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _userService.Login(loginDto);

            if (!result.IsSucced)
            {
                return BadRequest(result.Message);
            }

            return Ok(new
            {
                IsSucced = result.IsSucced,
                Token = result.Token, // Authentication için token döndürülür
                Message = result.Message,
            });
        }
    }


}
