using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using KidsAppBackend.Business.Operations.User;
using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Business.Types;
using KidsAppBackend.Data.Entities; 

namespace KidsAppBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var result = await _userService.UpdateUser(id, dto);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, [FromBody] JsonPatchDocument<ChildUser> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Patch document is null");

            var result = await _userService.PatchUser(id, patchDoc);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var result = await _userService.DeleteUserById(id);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpPost("{id}/score")]
        public async Task<IActionResult> SetScore(int id, [FromBody] ScoreDto dto)
        {
            var result = await _userService.SetScore(id, dto);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpPost("{childId}/favorite-book/{audioBookId}")]
        public async Task<IActionResult> AddFavoriteBook(int childId, int audioBookId)
        {
            var result = await _userService.AddFavoriteBookToChild(childId, audioBookId);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpGet("{childId}/favorite-books")]
        public async Task<IActionResult> GetFavorites(int childId)
        {
            var result = await _userService.GetFavoriteBooksOfChild(childId);
            if (!result.IsSucced) return BadRequest(result.Message);
            return Ok(result.Data);
        }
    }
}
