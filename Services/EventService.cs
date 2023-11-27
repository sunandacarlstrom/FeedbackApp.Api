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

    // TODO: Lägg till fallback ifall det inte går att hämta önskad data
    public async Task<List<Event>> GetAllEvents()
    {
        return await _context.Events
            .Find(_ => true)
            .ToListAsync();

        // var filter = Builders<Event>
        //     .Filter
        //     .Eq("CompanyId", companyId);
        // return await _context.Events.Find(filter).ToListAsync();
    }

    // TODO: Lägg till fallback ifall det inte går att hämta önskad data
    public async Task<List<Event>> GetCompanyEvents(string companyId)
    {
        return await _context.Events
            .Find(e => e.CompanyId == companyId)
            .ToListAsync();

        // var filter = Builders<Event>
        //     .Filter
        //     .Eq("CompanyId", companyId);
        // return await _context.Events.Find(filter).ToListAsync();
    }

    public async Task<Event> GetEventById(string id)
    {
        return await _context.Events
            .Find(e => e.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CreateEvent(Event companyEvent)
    {
        try
        {
            await _context.Events.InsertOneAsync(companyEvent);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fel vid inmatning av nytt event " + e);
            return false;
        }
    }

    public async Task<ReplaceOneResult> UpdateEvent(string id, Event companyEvent)
    {
        var result = await _context.Events
            .ReplaceOneAsync(e => e.Id == id, companyEvent);
        return result;

        //     FilterDefinition<Event> filter = Builders<Event>.Filter.Eq("Id", id);
        //     UpdateDefinition<Event> update = Builders<Event>.Update
        //     .Set(ce => ce.Name, companyEvent.Name)
        //     .Set(ce => ce.StartDate, companyEvent.StartDate)
        //     .Set(ce => ce.EndDate, companyEvent.EndDate)
        //     .Set(ce => ce.Location, companyEvent.Location);
        //     .Set(ce => ce.CompanyName, companyEvent.CompanyName);

        //     await _context.Events.UpdateOneAsync(filter, update);
        //     return;
    }

    public async Task<DeleteResult> DeleteEvent(string id)
    {
        return await _context.Events.DeleteOneAsync(e => e.Id == id);

        //     FilterDefinition<Event> filter = Builders<Event>.Filter.Eq("Id", id);
        //     await _context.Events.DeleteOneAsync(filter);
        //     return;
    }

    public async Task<DeleteResult> DeleteAllCompanyEvents(string companyId)
    {
        return await _context.Events.DeleteManyAsync(e => e.Id == companyId);

        //     FilterDefinition<Event> filter = Builders<Event>.Filter.Eq("Id", id);
        //     await _context.Events.DeleteOneAsync(filter);
        //     return;
    }
}
