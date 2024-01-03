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

    public async Task<List<Company>> GetCompanies()
    {
        return await _context.Companies.Find(new BsonDocument()).ToListAsync();
    }
}