using FeedbackApp.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FeedbackApp.Api.Data;
public class FeedbackAppContext
{
    public IMongoCollection<User> Users { get; private set; }
    public IMongoCollection<Company> Companies { get; private set; }
    public IMongoCollection<Event> Events { get; private set; }

    public FeedbackAppContext(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

        Users = database.GetCollection<User>(mongoDBSettings.Value.UserCollection);
        Companies = database.GetCollection<Company>(mongoDBSettings.Value.CompanyCollection);
        Events = database.GetCollection<Event>(mongoDBSettings.Value.EventCollection);
    }
}