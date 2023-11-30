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
        if (!ModelState.IsValid) return BadRequest("Information is missing in order to create an event");

        var result = await _eventService.CreateEvent(companyEvent);

        if (!result)
        {
            return StatusCode(500, "The event can't be created. Try again later!");
        }

        return CreatedAtAction(nameof(GetEventById), new { id = companyEvent.Id }, companyEvent);
    }

    [HttpPost("{eventId}/{quizIndex}/{questionIndex}")]
    public async Task<IActionResult> AddAnswer(string eventId, int quizIndex, int questionIndex, [FromBody] List<string> result)
    {
        try
        {
            var updateResult = await _eventService.AddAnswer(eventId, quizIndex, questionIndex, result);

            if (updateResult.ModifiedCount > 0)
            {
                return Ok("Answer is added successfully!");
            }
            else
            {
                return NotFound("The event isn't found or the answer isn't added.");
            }
        }
        catch (Exception ex)
        {
            // TODO: Logga fel istället för att endast retunera ett felmeddelande
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPost("{eventId}/{quizIndex}")]
    public async Task<IActionResult> AddQuestion(string eventId, int quizIndex, [FromBody] Question questions)
    {
        try
        {
            var updateResult = await _eventService.AddQuestion(eventId, quizIndex, questions);

            if (updateResult.ModifiedCount > 0)
            {
                return Ok("Question is added successfully!");
            }
            else
            {
                return NotFound("The event isn't found or the answer isn't added.");
            }
        }
        catch (Exception ex)
        {
            // TODO: Logga fel istället för att endast retunera ett felmeddelande
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(string id, [FromBody] Event companyEvent)
    {
        if (!ModelState.IsValid) return BadRequest("Information is missing in order to update an event");

        var updatedEvent = await _eventService.UpdateEvent(id, companyEvent);

        if (updatedEvent != null)
        {
            return Ok(updatedEvent);
        }
        else
        {
            return NotFound("Can't update the selected event");
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
            return NotFound("The selected event isn't found");
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
