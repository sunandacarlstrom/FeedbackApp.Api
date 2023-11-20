using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsAndReviewsController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;
    public ListingsAndReviewsController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;

    }

    [HttpGet]
    public async Task<List<Company>> Get()
    {
        return await _mongoDBService.GetAsync();

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Company listingsAndReviews)
    {
        await _mongoDBService.CreateAsync(listingsAndReviews);

        return CreatedAtAction(nameof(Get), new { id = listingsAndReviews.Id }, listingsAndReviews);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToListingsAndReviews(string id, [FromBody] string listingsAndReviewsId)
    {
        await _mongoDBService.AddToListingsAndReviewsAsync(id, listingsAndReviewsId);

        return NoContent();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }
}