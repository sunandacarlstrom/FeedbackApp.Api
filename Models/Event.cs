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
    public string? Name { get; set; }

    [BsonElement("start_date")]
    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [BsonElement("end_date")]
    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }

    [BsonElement("location")]
    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [BsonElement("company_full_name")]
    [JsonPropertyName("company_full_name")]
    public string? CompanyName { get; set; }

    [BsonElement("company_id")]
    [JsonPropertyName("company_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CompanyId { get; set; }

    [BsonElement("quizzes")]
    [JsonPropertyName("quizzes")]
    public List<Quiz>? Quizzes { get; set; }
}