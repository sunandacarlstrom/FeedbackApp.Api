using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly CompanyService _companyService;
    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<List<Company>> Get()
    {
        return await _companyService.GetCompany();

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Company company)
    {
        await _companyService.CreateCompany(company);

        return CreatedAtAction(nameof(Get), new { id = company.Id }, company);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToListingsAndReviews(string id, [FromBody] string companyId)
    {
        await _companyService.UpdateCompany(id, companyId);

        return NoContent();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _companyService.DeleteCompany(id);
        return NoContent();
    }
}