using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Business.Operations.User;

namespace KidsAppBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] // Tüm action'lar anonim erişime izin verir
    public class AudioAnimalController : ControllerBase
    {
        private readonly IUserService _userService;

        public AudioAnimalController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimalSound([FromBody] CreateAnimalSoundDto dto)
        {
            var result = await _userService.AddAnimalSound(dto);
            if (!result.IsSucced)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    }
}
