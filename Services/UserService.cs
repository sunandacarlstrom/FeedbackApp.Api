using FeedbackApp.Api.Data;
using FeedbackApp.Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FeedbackApp.Api.Services;
public class UserService
{
    private readonly FeedbackContext _context;
    public UserService(FeedbackContext context)
    {
        _context = context;
    }

    public async Task CreateUser(User user)
    {
        await _context.Users.InsertOneAsync(user);
        return;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _context.Users.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateUser(string id, string userId)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        UpdateDefinition<User> update = Builders<User>.Update.AddToSet<string>(_context.Users.CollectionNamespace.CollectionName, userId);
        await _context.Users.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteUser(string id)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        await _context.Users.DeleteOneAsync(filter);
        return;
    }
}