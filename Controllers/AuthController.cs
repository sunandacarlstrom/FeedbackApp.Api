using FeedbackApp.Api.Dto;
using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Api.Controllers;

[ApiController]
[Authorize(Policy = "Admin")]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserService _userService;

    public AuthController(JwtSettings jwtSettings, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, UserService userService)
    {
        _userService = userService;
        _jwtSettings = jwtSettings;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            return BadRequest("No entered username or password");

        User? dbUser = await _userService.GetLoginCredentials(login);

        if (dbUser == null)
            return Unauthorized();

        if (!BCrypt.Net.BCrypt.Verify(login.Password, dbUser.Password))
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(dbUser);
        string serializedToken = _jwtSettings.GenerateJwt(dbUser, roles);

        return Ok(new { Token = serializedToken });
    }
}