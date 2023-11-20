using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Answer
{
    [BsonElement("answer")]
    [JsonPropertyName("answer")]
    public string? Text { get; set; }

    [BsonElement("user_name")]
    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    [BsonElement("user_id")]
    [JsonPropertyName("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
}
