using System.IdentityModel.Tokens.Jwt;
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

    public async Task<User?> GetUserById(string id)
    {
        try
        {
            return await _context.Users
            .Find(u => u.Id.ToString() == id)
            .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Can't get the user with id: {id} due to {ex.Message}");
            return null;
        }
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        try
        {
            return await _context.Users
            .Find(u => u.Email == email)
            .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Can't get the user with email: {email} due to {ex.Message}");
            return null;
        }
    }

    public async Task<User> GetUserFromToken(string token, JwtSettings jwtSettings)
    {
        var id = jwtSettings.GetClaim(token, JwtRegisteredClaimNames.Sub);
        return await GetUserById(id);
    }

    public async Task<User?> GetLoginCredentials(CredentialsDto login)
    {
        try
        {
            return await _context.Users
                .Find(u => u.Email == login.Email)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Can't get the user's login credentials due to {ex.Message}");
            return null;
        }
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
        .Set(u => u.PasswordHash, user.PasswordHash)
        .Set(u => u.CompanyRoles, user.CompanyRoles);

        await _context.Users.UpdateOneAsync(filter, update);
        return;
    }

    // public async Task UpdateUserPermission(string id, UserPermissionDto model)
    // {
    //     FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
    //     UpdateDefinition<User> update = Builders<User>.Update.Set(u => u.Permission, model.Permission);

    //     await _context.Users.UpdateOneAsync(filter, update);
    //     return;
    // }

    public async Task DeleteUser(string id)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        await _context.Users.DeleteOneAsync(filter);
        return;
    }
}