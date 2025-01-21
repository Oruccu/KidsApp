using KidsAppBackend.Data.Repositories;
using KidsAppBackend.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KidsAppBackend.Business.Operations.User.Dtos;
using KidsAppBackend.Business.Operations.User;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Parent,Child")] 
public class AudioBookController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AudioBookController> _logger;

    public AudioBookController(
        IUserService userService,
        ILogger<AudioBookController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

 [HttpGet("{id}")]
    public async Task<IActionResult> GetAudioBookById(int id)
    {
        try
        {
            var result = await _userService.GetAudioBook(id);
            if (!result.IsSucced)
            {
                _logger.LogWarning($"AudioBook with Id {id} not found.");
                return NotFound(new { Message = result.Message });
            }

            var audioBookDto = result.Data as AudioBookDto;
            return Ok(audioBookDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching AudioBook with Id {id}.");
            return StatusCode(500, new { Message = "An error occurred while processing your request." });
        }
    }
}
