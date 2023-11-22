using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("company")]
    [JsonPropertyName("company")]
    public string? CompanyName { get; set; }

    [BsonElement("business_area")]
    [JsonPropertyName("business_area")]
    public string? BusinessArea { get; set; }
    
    [BsonElement("username")]
    [JsonPropertyName("username")]
    public string? UserName { get; set; }
    
    [BsonElement("password")]
    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
