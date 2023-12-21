using System.Text.Json.Serialization;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace FeedbackApp.Api.Models;

[CollectionName("users")]
public class User : MongoIdentityUser<ObjectId>
{
    [BsonElement("first_name")]
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [BsonElement("last_name")]
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [BsonElement("company_roles")]
    [JsonPropertyName("company_roles")]
    public List<CompanyRole>? CompanyRoles { get; set; }
}
