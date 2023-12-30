using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Api.Controllers;

[ApiController]
[Authorize(Policy = "User")]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly CompanyService _companyService;
    private readonly UserService _userService;
    private readonly JwtSettings _jwtSettings;
    public CompaniesController(CompanyService companyService, UserService userService, JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
        _userService = userService;
        _companyService = companyService;
    }

    [HttpGet("/all")]
    public async Task<List<Company>> GetAllCompanies()
    {
        return await _companyService.GetCompanies();
    }

    [HttpGet()]
    public async Task<List<CompanyRole>> GetUserCompanies()
    {
        var token = _userService.GetTokenFromWebBrowser(HttpContext);
        var user = await _userService.GetUserFromToken(token, _jwtSettings);
        if(user == null || user.CompanyRoles == null)
            return new List<CompanyRole>();

        return user.CompanyRoles;
    }
}