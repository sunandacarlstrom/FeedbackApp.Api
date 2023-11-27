using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<List<Event>> GetAllEvents()
    {
        return await _eventService.GetAllEvents();
    }

    [HttpGet("getcompanyevents/{companyId}")]
    public async Task<List<Event>> GetCompanyEvents(string companyId)
    {
        return await _eventService.GetCompanyEvents(companyId);
    }

    [HttpGet("{id}")]
    public async Task<Event> GetEventById(string id)
    {
        return await _eventService.GetEventById(id);
    }

    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] Event companyEvent)
    {
        var result = await _eventService.CreateEvent(companyEvent);

        if (!result)
        {
            return StatusCode(500, "The event could not be created. Try again!");
        }

        return CreatedAtAction(nameof(GetEventById), new { id = companyEvent.Id }, companyEvent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(string id, [FromBody] Event companyEvent)
    {
        if (companyEvent == null)
        {
            return BadRequest("The event is not filled in correctly");
        }

        var updatedEvent = await _eventService.UpdateEvent(id, companyEvent);

        if (updatedEvent != null)
        {
            return Ok(updatedEvent);
        }
        else
        {
            return NotFound("Could not update the selected event");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        var result = await _eventService.DeleteEvent(id);

        if (result.DeletedCount > 0)
        {
            return Ok("The event was successfully deleted");
        }
        else
        {
            return NotFound("The selected event is not found");
        }
    }

    [HttpDelete("company/{companyId}")]
    public async Task<IActionResult> DeleteAllCompanyEvents(string companyId)
    {
        var result = await _eventService.DeleteAllCompanyEvents(companyId);

        if (result.DeletedCount > 0)
        {
            return Ok($"You deleted {result.DeletedCount} events for company {companyId}");
        }
        else
        {
            return NotFound($"There are no events found for company {companyId}");
        }
    }
}
