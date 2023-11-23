using System.Security;
using FeedbackApp.Api.Dto;
using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers()
    {
        return await _userService.GetUsers();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        await _userService.CreateUser(user);

        return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
    {
        await _userService.UpdateUser(id, user);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUserPermission(string id, [FromBody] UserPermissionDto model)
    {
        await _userService.UpdateUserPermission(id, model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _userService.DeleteUser(id);
        return NoContent();
    }
}
