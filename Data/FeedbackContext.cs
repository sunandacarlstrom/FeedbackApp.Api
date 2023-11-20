using FeedbackApp.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FeedbackApp.Api.Data;
public class FeedbackContext
{
    public IMongoCollection<Company> Companies { get; private set; }
    public IMongoCollection<User> Users { get; private set; }
    public IMongoCollection<Event> Events { get; private set; }

    public FeedbackContext(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

        Companies = database.GetCollection<Company>(mongoDBSettings.Value.Collections[0]);
        Events = database.GetCollection<Event>(mongoDBSettings.Value.Collections[1]);
        Users = database.GetCollection<User>(mongoDBSettings.Value.Collections[2]);
    }
}