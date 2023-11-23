using FeedbackApp.Api.Data;
using FeedbackApp.Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FeedbackApp.Api.Services;
public class EventService
{
    private readonly FeedbackAppContext _context;
    public EventService(FeedbackAppContext context)
    {
        _context = context;
    }
}