using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace FeedbackApp.Api.Models;

[CollectionName("users")]
public class User : MongoIdentityUser<ObjectId>
{
    [Required]
    [BsonElement("username")]
    [JsonPropertyName("username")]
    public string? UserName { get; set; }

    [Required]
    [BsonElement("password")]
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [Required]
    [BsonElement("permission")]
    [JsonPropertyName("permission")]
    public string? Permission { get; set; }

    [BsonElement("company_permissions")]
    [JsonPropertyName("company_permissions")]
    public List<CompanyPermissions>? CompanyPermissions { get; set; }
}
