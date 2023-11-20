using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("first_name")]
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }
    
    [BsonElement("last_name")]
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }
    
    [BsonElement("email_address")]
    [JsonPropertyName("email_address")]
    public string? EmailAddress { get; set; }
}
