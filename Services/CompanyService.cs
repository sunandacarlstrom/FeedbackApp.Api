using FeedbackApp.Api.Data;
using FeedbackApp.Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FeedbackApp.Api.Services;
public class CompanyService
{
    private readonly FeedbackContext _context;
    public CompanyService(FeedbackContext context)
    {
        _context = context;
    }

    public async Task CreateCompany(Company company)
    {
        await _context.Companies.InsertOneAsync(company);
        return;
    }

    public async Task<List<Company>> GetCompany()
    {
        return await _context.Companies.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateCompany(string id, string companyId)
    {
        FilterDefinition<Company> filter = Builders<Company>.Filter.Eq("Id", id);
        UpdateDefinition<Company> update = Builders<Company>.Update.AddToSet<string>("company", companyId);
        await _context.Companies.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteCompany(string id)
    {
        FilterDefinition<Company> filter = Builders<Company>.Filter.Eq("Id", id);
        await _context.Companies.DeleteOneAsync(filter);
        return;
    }
}