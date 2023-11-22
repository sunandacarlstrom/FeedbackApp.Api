using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Question
{
    [BsonElement("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [BsonElement("type")]
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [BsonElement("options")]
    [JsonPropertyName("options")]
    public List<string>? Options { get; set; }

    [BsonElement("answers")]
    [JsonPropertyName("answers")]
    public List<Answer>? Answers { get; set; }
}
