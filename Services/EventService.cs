using FeedbackApp.Api.Data;
using FeedbackApp.Api.Models;
using MongoDB.Driver;

namespace FeedbackApp.Api.Services;
public class EventService
{
    private readonly FeedbackAppContext _context;
    public EventService(FeedbackAppContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> GetAllEvents()
    {
        try
        {
            return await _context.Events
            .Find(_ => true)
            .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Can't get any events in the collection due to {ex.Message}");
            return new List<Event>();
        }
    }

    public async Task<List<Event>> GetCompanyEvents(string companyId)
    {
        try
        {
            return await _context.Events
           .Find(e => e.CompanyId == companyId)
           .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Can't get the events of the company {companyId} due to {ex.Message}");
            return new List<Event>();
        }
    }

    public async Task<Event> GetEventById(string id)
    {
        try
        {
            return await _context.Events
            .Find(e => e.Id == id)
            .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Can't get the event with id: {id} due to {ex.Message}");
            return new Event();
        }
    }

    public async Task<Quiz> GetQuizById(string eventId, int quizIndex)
    {
        var companyEvent = await GetEventById(eventId);
        var selectedQuiz = companyEvent.Quizzes[quizIndex];
        return selectedQuiz;
    }

    public async Task<Question> GetQuestionById(string eventId, int quizIndex, int questionIndex)
    {
        var companyEvent = await GetEventById(eventId);
        var selectedQuestion = companyEvent.Quizzes[quizIndex].Questions[questionIndex];
        return selectedQuestion;
    }

    public async Task<bool> AddEvent(Event companyEvent)
    {
        try
        {
            await _context.Events.InsertOneAsync(companyEvent);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Can't create a new event due to {ex.Message}");
            return false;
        }
    }

    public async Task<UpdateResult> AddQuestion(string eventId, int quizIndex, Question questions)
    {
        var addQuestion = Builders<Event>.Update.Push(e => e.Quizzes[quizIndex].Questions, questions);

        var updateResult = await _context.Events.UpdateOneAsync(e => e.Id == eventId, addQuestion);
        return updateResult;
    }

    public async Task<UpdateResult> AddAnswer(string eventId, int quizIndex, int questionIndex, List<string> result)
    {
        var answers = new Answer
        {
            Result = result
        };

        var addAnswer = Builders<Event>.Update.Push(e => e.Quizzes[quizIndex].Questions[questionIndex].Answers, answers);

        var updateResult = await _context.Events.UpdateOneAsync(e => e.Id == eventId, addAnswer);
        return updateResult;
    }

    public async Task<ReplaceOneResult> UpdateEvent(string id, Event companyEvent)
    {
        var result = await _context.Events
            .ReplaceOneAsync(e => e.Id == id, companyEvent);
        return result;
    }

    public async Task<UpdateResult> EditQuestion(string eventId, int quizIndex, int questionIndex, Question updatedQuestion)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, eventId);

        var update = Builders<Event>.Update
            .Set(e => e.Quizzes[quizIndex].Questions[questionIndex].Title, updatedQuestion.Title)
            .Set(e => e.Quizzes[quizIndex].Questions[questionIndex].Type, updatedQuestion.Type)
            .Set(e => e.Quizzes[quizIndex].Questions[questionIndex].Options, updatedQuestion.Options)
            .Set(e => e.Quizzes[quizIndex].Questions[questionIndex].Answers, updatedQuestion.Answers);

        var result = await _context.Events.UpdateOneAsync(filter, update);
        return result;
    }

    public async Task<DeleteResult> DeleteAllCompanyEvents(string companyId)
    {
        return await _context.Events.DeleteManyAsync(e => e.Id == companyId);
    }

    public async Task<DeleteResult> DeleteEvent(string id)
    {
        return await _context.Events.DeleteOneAsync(e => e.Id == id);
    }

    public async Task<UpdateResult> DeleteQuestion(string eventId, int quizIndex, int questionIndex)
    {
        var questionToRemove = await GetQuestionById(eventId, quizIndex, questionIndex);

        var filter = Builders<Event>.Filter
            .Where(e => e.Id == eventId);

        var update = Builders<Event>.Update
            .Pull(e => e.Quizzes[quizIndex].Questions, questionToRemove);

        return await _context.Events.UpdateOneAsync(filter, update);
    }
}
