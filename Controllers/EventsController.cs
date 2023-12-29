using FeedbackApp.Api.Dto;
using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Api.Controllers;

[ApiController]
[Authorize(Policy = "User")]
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
    public async Task<IActionResult> GetCompanyEvents(string companyId)
    {
        var events = await _eventService.GetCompanyEvents(companyId);
        var eventList = events.Select(e => new
        {
            e.Id,
            e.Name,
        });

        return Ok(eventList);
    }

    [HttpGet("getquizzes/{id}")]
    public async Task<IActionResult> GetQuizzes(string id)
    {
        var quizzes = await _eventService.GetQuizzes(id);
        return Ok(quizzes);
    }

    [HttpGet("{id}")]
    public async Task<Event> GetEventById(string id)
    {
        return await _eventService.GetEventById(id);
    }

    [HttpGet("{eventId}/{quizIndex}")]
    public async Task<Quiz> GetQuizById(string eventId, int quizIndex)
    {
        return await _eventService.GetQuizById(eventId, quizIndex);
    }

    [HttpGet("{eventId}/{quizIndex}/{questionIndex}")]
    public async Task<QuestionDto> GetQuestionById(string eventId, int quizIndex, int questionIndex)
    {
        return await _eventService.GetQuestionById(eventId, quizIndex, questionIndex);
    }

    [HttpGet("{eventId}/{quizIndex}/{questionIndex}/details")]
    public async Task<Question> GetQuestionByIdDetails(string eventId, int quizIndex, int questionIndex)
    {
        return await _eventService.GetQuestionByIdDetails(eventId, quizIndex, questionIndex);
    }

    [HttpGet("startSession")]
    public IActionResult GetSession()
    {
        return new JsonResult(new { SessionId = Guid.NewGuid() });
    }

    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] Event companyEvent)
    {
        if (!ModelState.IsValid) return BadRequest("Information is missing in order to create an event");

        var result = await _eventService.AddEvent(companyEvent);

        if (!result)
        {
            return StatusCode(500, "The event can't be created. Try again later!");
        }

        return CreatedAtAction(nameof(GetEventById), new { id = companyEvent.Id }, companyEvent);
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

    [HttpPost("{eventId}/{quizIndex}/{questionIndex}")]
    public async Task<IActionResult> AddAnswer(string eventId, int quizIndex, int questionIndex, [FromBody] Answer answer)
    {
        try
        {
            var updateResult = await _eventService.AddAnswer(eventId, quizIndex, questionIndex, answer);

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

    [HttpPatch("{eventId}/{quizIndex}/{questionIndex}")]
    public async Task<IActionResult> EditQuestion(string eventId, int quizIndex, int questionIndex, [FromBody] Question updatedQuestion)
    {
        var result = await _eventService.EditQuestion(eventId, quizIndex, questionIndex, updatedQuestion);

        if (result.ModifiedCount > 0)
        {
            return Ok("Question updated successfully");
        }
        else
        {
            return NotFound("Can't edit the question. Try again!");
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

    [HttpDelete("{eventId}/{quizIndex}/{questionIndex}")]
    public async Task<IActionResult> DeleteQuestion(string eventId, int quizIndex, int questionIndex)
    {
        var result = await _eventService.DeleteQuestion(eventId, quizIndex, questionIndex);

        if (result.ModifiedCount > 0)
        {
            return Ok("The selected question was successfully deleted");
        }
        else
        {
            return NotFound("The selected question isn't found");
        }
    }
}
