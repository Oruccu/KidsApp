using Microsoft.AspNetCore.Authorization; 
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using KidsAppBackend.Business.Operations.User;
using KidsAppBackend.Business.Operations.User.Dtos;

namespace KidsAppBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tüm action'lar token ister
    public class KidsModeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<KidsModeController> _logger;

        public KidsModeController(IUserService userService, ILogger<KidsModeController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateKidsMode([FromBody] KidsModeDto kidsModeDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid.");
                return BadRequest(ModelState);
            }

            try
            {
                // 1) Token’dan "sub" claim'ini alıyoruz (ChildId).
                var userIdString = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    _logger.LogError("Token does not contain 'sub' claim.");
                    return Unauthorized("Token does not contain 'sub' claim.");
                }

                // 2) Body'deki ChildId yerine, token'daki ChildId’yi setliyoruz.
                kidsModeDto.ChildId = int.Parse(userIdString);

                // 3) Servis katmanına gönderip DB'ye ekliyoruz.
                var result = await _userService.CreateKidsModeAsync(kidsModeDto);
                if (result == null)
                {
                    _logger.LogError("Failed to create KidsMode.");
                    return BadRequest("Failed to create KidsMode.");
                }

                _logger.LogInformation("KidsMode successfully created.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating KidsMode.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateKidsMode([FromBody] KidsModeDto kidsModeDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid.");
                return BadRequest(ModelState);
            }

            try
            {
                // Token’dan ChildId alıyoruz.
                var userIdString = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    return Unauthorized("Token does not contain 'sub' claim.");
                }

                // Güncellenen verinin asıl sahibinin ID'sini token’dan setliyoruz:
                kidsModeDto.ChildId = int.Parse(userIdString);

                var result = await _userService.UpdateKidsModeAsync(kidsModeDto);
                if (result == null)
                {
                    _logger.LogError("KidsMode not found or not updated.");
                    return NotFound("KidsMode not found.");
                }
                _logger.LogInformation("KidsMode successfully updated.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating KidsMode.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{childId}")]
        public async Task<IActionResult> GetKidsMode(int childId)
        {
            try
            {
                // Burada route'tan childId geliyor, 
                // ama yine de token kimliğini de kontrol edebilirsiniz:
                var userIdString = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    return Unauthorized("Token does not contain 'sub' claim.");
                }

                // İsteğe göre route'taki childId ile userIdString eşleşmezse hata verebilirsiniz.
                // Ama eğer her çocuk kendi kaydını görmek zorundaysa:
                // if (int.Parse(userIdString) != childId)
                // {
                //     return Forbid("You cannot view another child's data.");
                // }

                var result = await _userService.GetKidsModeByIdAsync(childId);
                if (result == null)
                {
                    _logger.LogError("KidsMode not found.");
                    return NotFound("KidsMode not found.");
                }
                _logger.LogInformation("KidsMode successfully retrieved.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching KidsMode.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
