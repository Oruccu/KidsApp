using KidsAppBackend.Business.Operations.User;
using KidsAppBackend.Business.Operations.User.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KidsAppBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidsModeController : ControllerBase
    {
        private readonly IUserService _userService;

        public KidsModeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateKidsMode([FromBody] KidsModeDto kidsModeDto)
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
                return Unauthorized("User ID could not be determined.");

            kidsModeDto.ChildId = int.Parse(userId);

            var result = await _userService.CreateKidsModeAsync(kidsModeDto);
            if (result == null) return BadRequest("Failed to create KidsMode.");
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateKidsMode([FromBody] KidsModeDto kidsModeDto)
        {
            var result = await _userService.UpdateKidsModeAsync(kidsModeDto);
            if (result == null) return NotFound("KidsMode not found.");
            return Ok(result);
        }

        [HttpGet("{childId}")]
        public async Task<IActionResult> GetKidsMode(int childId)
        {
            var result = await _userService.GetKidsModeByIdAsync(childId);
            if (result == null) return NotFound("KidsMode not found.");
            return Ok(result);
        }
    }
}
