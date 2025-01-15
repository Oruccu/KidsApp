using Microsoft.AspNetCore.Authorization; 
using System.Security.Claims;
using KidsAppBackend.Business.Operations.User;
using KidsAppBackend.Business.Operations.User.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KidsAppBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] 
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
                var result = await _userService.UpdateKidsModeAsync(kidsModeDto);
                if (result == null)
                {
                    _logger.LogError("KidsMode not found.");
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
