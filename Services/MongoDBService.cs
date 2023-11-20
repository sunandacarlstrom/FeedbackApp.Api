using FeedbackApp.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FeedbackApp.Api.Services;
public class MongoDBService
{
    private readonly IMongoCollection<Company> _companyCollection;
    private readonly IMongoCollection<User> _userCollection;
    private readonly IMongoCollection<Event> _eventCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

        _companyCollection = database.GetCollection<Company>(mongoDBSettings.Value.Collections[0]);
        _eventCollection = database.GetCollection<Event>(mongoDBSettings.Value.Collections[1]);
        _userCollection = database.GetCollection<User>(mongoDBSettings.Value.Collections[2]);
    }

    public async Task CreateCompany(Company company)
    {
        await _companyCollection.InsertOneAsync(company);
        return;
    }

    public async Task<List<Company>> GetCompany()
    {
        return await _companyCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateCompany(string id, string companyId)
    {
        FilterDefinition<Company> filter = Builders<Company>.Filter.Eq("Id", id);
        UpdateDefinition<Company> update = Builders<Company>.Update.AddToSet<string>("company", companyId);
        await _companyCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteCompany(string id)
    {
        FilterDefinition<Company> filter = Builders<Company>.Filter.Eq("Id", id);
        await _companyCollection.DeleteOneAsync(filter);
        return;
    }
}