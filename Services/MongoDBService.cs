using FeedbackApp.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FeedbackApp.Api.Services;
public class MongoDBService
{
    private readonly IMongoCollection<Company> _listingsAndReviewsCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _listingsAndReviewsCollection = database.GetCollection<Company>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Company listingsAndReviews)
    {
        await _listingsAndReviewsCollection.InsertOneAsync(listingsAndReviews);
        return;
    }

    public async Task<List<Company>> GetAsync()
    {
        return await _listingsAndReviewsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToListingsAndReviewsAsync(string id, string listingsAndReviewsId)
    {
        FilterDefinition<Company> filter = Builders<Company>.Filter.Eq("Id", id);
        UpdateDefinition<Company> update = Builders<Company>.Update.AddToSet<string>("reviews", listingsAndReviewsId);
        await _listingsAndReviewsCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Company> filter = Builders<Company>.Filter.Eq("Id", id);
        await _listingsAndReviewsCollection.DeleteOneAsync(filter);
        return;
    }
}