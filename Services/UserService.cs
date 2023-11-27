using FeedbackApp.Api.Data;
using FeedbackApp.Api.Dto;
using FeedbackApp.Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FeedbackApp.Api.Services;
public class UserService
{
    private readonly FeedbackAppContext _context;
    public UserService(FeedbackAppContext context)
    {
        _context = context;
    }

    // TODO: Ändra alla endpoints i UserService + UserController
    public async Task<List<User>> GetUsers()
    {
        return await _context.Users.Find(new BsonDocument()).ToListAsync();
    }

    public async Task CreateUser(User user)
    {
        await _context.Users.InsertOneAsync(user);
        return;
    }

    public async Task UpdateUser(string id, User user)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        UpdateDefinition<User> update = Builders<User>.Update
        .Set(u => u.UserName, user.UserName)
        .Set(u => u.Password, user.Password)
        .Set(u => u.Permission, user.Permission)
        .Set(u => u.CompanyPermissions, user.CompanyPermissions);

        await _context.Users.UpdateOneAsync(filter, update);
        return;
    }

    public async Task UpdateUserPermission(string id, UserPermissionDto model)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        UpdateDefinition<User> update = Builders<User>.Update.Set(u => u.Permission, model.Permission);

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