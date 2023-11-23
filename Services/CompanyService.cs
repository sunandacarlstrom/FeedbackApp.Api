using FeedbackApp.Api.Data;
using FeedbackApp.Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FeedbackApp.Api.Services;
public class CompanyService
{
    private readonly FeedbackAppContext _context;
    public CompanyService(FeedbackAppContext context)
    {
        _context = context;
    }
}