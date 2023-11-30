using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

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

    [Required]
    [BsonElement("company_permissions")]
    [JsonPropertyName("company_permissions")]
    public List<CompanyPermissions>? CompanyPermissions { get; set; }
}
