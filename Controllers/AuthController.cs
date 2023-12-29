using FeedbackApp.Api.Dto;
using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserService _userService;

    public AuthController(JwtSettings jwtSettings, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, UserService userService)
    {
        _userService = userService;
        _jwtSettings = jwtSettings;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [Authorize(Policy = "Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser)
    {
        if (registerUser == null)
            return BadRequest("Invalid user data");

        if (string.IsNullOrEmpty(registerUser.Email) || string.IsNullOrEmpty(registerUser.Password))
            return BadRequest("Invalid username or password");

        User newUser = new User
        {
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            Email = registerUser.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUser.Password),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        try
        {
            await _userService.CreateUser(newUser);

            var roleResult = await _userService.AddRoleToUser(newUser.Id, "User");

            if (roleResult == null)
            {
                return BadRequest("Can't find role or user");
            }

            if (roleResult.IsAcknowledged || roleResult.MatchedCount > 0 || roleResult.ModifiedCount > 0)
            {
                return CreatedAtAction(nameof(Register), newUser);
            }

            return BadRequest($"Can't assign role to user due to {roleResult}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Can't create user due to {ex.Message}");
            throw;
        }
    }

    [HttpPost("login")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
    {
        if (string.IsNullOrEmpty(loginUser.Email) || string.IsNullOrEmpty(loginUser.Password))
            return BadRequest("No entered email or password");

        User? dbUser = await _userService.GetLoginCredentials(loginUser);

        if (dbUser == null)
            return Unauthorized();

        if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, dbUser.PasswordHash))
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(dbUser);
        string serializedToken = _jwtSettings.GenerateJwt(dbUser, roles);

        return Ok(new { Token = serializedToken });
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out successfully");
    }
}