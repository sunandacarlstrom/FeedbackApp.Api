using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Event
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string? EventName { get; set; }

    [BsonElement("company")]
    [JsonPropertyName("company")]
    public string? CompanyName { get; set; }

    [BsonElement("business_area")]
    [JsonPropertyName("business_area")]
    public string? BusinessArea { get; set; }

    [BsonElement("user_id")]
    [JsonPropertyName("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }

    [BsonElement("quizzes")]
    [JsonPropertyName("quizzes")]
    public List<Quiz>? Quizzes { get; set; }
}