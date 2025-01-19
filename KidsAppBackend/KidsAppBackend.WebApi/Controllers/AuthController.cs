using Microsoft.AspNetCore.Mvc;
using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Business.Operations.User;
using System.Threading.Tasks;
using System.Linq;
using KidsAppBackend.WebApi.Models;
using KidsAppBackend.Business.Utilities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace KidsAppBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenBlacklist _tokenBlacklist;

        public AuthController(IUserService userService, ITokenBlacklist tokenBlacklist)
        {
            _userService = userService;
            _tokenBlacklist = tokenBlacklist;
        }

        // ------------------- Kayıt (Register) -------------------
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
                return BadRequest(result.Message);
            }
            return Ok(new { IsSucced = result.IsSucced, Message = result.Message });
        }

        // ------------------- Giriş (Login) -------------------
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
                Token = result.Token,
                Message = result.Message,
                ChildId = result.ChildId
            });
        }

        // ------------------- Parent Login -------------------
        [HttpPost("parentLogin")]
        public async Task<IActionResult> ParentLogin([FromBody] ParentLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var loginDto = new LoginDto
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _userService.ParentLogin(loginDto);
            if (!result.IsSucced)
            {
                return BadRequest(result.Message);
            }

            return Ok(new
            {
                IsSucced = result.IsSucced,
                Token = result.Token,
                UserName = result.UserName,
                Message = result.Message
            });
        }

        // ------------------- Çıkış (Logout) -------------------
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"]
                                     .FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required for logout.");
            }

            _tokenBlacklist.AddToBlacklist(token);
            return Ok(new { Message = "Logout successful." });
        }

        [HttpPost("{id}/logout")]
        public async Task<IActionResult> LogoutById(int id)
        {
            var result = await _userService.LogoutById(id);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        // ------------------- Geçerli Kullanıcı -------------------
        [HttpGet("getCurrentUser")]
        public IActionResult GetCurrentUser()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
            if (username == null)
            {
                return Unauthorized("User is not logged in or token is invalid.");
            }
            return Ok(new { UserName = username });
        }
    }
}
