using FeedbackApp.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FeedbackApp.Api.Data;
public class FeedbackContext
{
    public IMongoCollection<User> Users { get; private set; }
    public IMongoCollection<Event> Events { get; private set; }

    public FeedbackContext(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

        Users = database.GetCollection<User>(mongoDBSettings.Value.UserCollection);
        Events = database.GetCollection<Event>(mongoDBSettings.Value.EventCollection);
    }
}