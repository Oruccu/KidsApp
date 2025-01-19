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
            try
            {
                var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    _logger.LogError("Invalid or missing user ID in token");
                    return Unauthorized(new { Message = "Invalid token" });
                }
                kidsModeDto.ChildId = userId;
                var existingMode = await _userService.GetKidsModeByIdAsync(userId);
                if (existingMode != null)
                {
                    var result = await _userService.UpdateKidsModeAsync(kidsModeDto);
                    return Ok(result);
                }

                // Kayıt yoksa yeni oluştur
                var createResult = await _userService.CreateKidsModeAsync(kidsModeDto);
                return Ok(createResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateKidsMode");
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost("{childId}/kidsMode")]
        public async Task<IActionResult> AddKidsMode(int childId, [FromBody] KidsModeDto dto)
        {
            var result = await _userService.AddKidsMode(childId, dto);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Message);
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
                var userIdString = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    return Unauthorized("Token does not contain 'sub' claim.");
                }

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

        [HttpPut("{childId}/kidsMode")]
        public async Task<IActionResult> UpdateKidsMode(int childId, [FromBody] KidsModeDto dto)
        {
            var result = await _userService.UpdateKidsMode(childId, dto);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpGet("{childId}")]
        public async Task<IActionResult> GetKidsMode(int childId)
        {
            try
            {
                var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized(new { Message = "Invalid token" });
                }

                if (userId != childId)
                {
                    return Forbid();
                }

                var result = await _userService.GetKidsModeByIdAsync(childId);
                if (result == null)
                {
                    var defaultMode = new KidsModeDto
                    {
                        ChildId = childId,
                        Mode = "Girl"
                    };
                    result = await _userService.CreateKidsModeAsync(defaultMode);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetKidsMode");
                return StatusCode(500, new { Message = ex.Message });
            }
        }
        [HttpDelete("{childId}/kidsMode")]
        public async Task<IActionResult> DeleteKidsMode(int childId)
        {
            var result = await _userService.DeleteKidsMode(childId);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }

}
