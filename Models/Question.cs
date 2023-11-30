using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Question
{
    [Required]
    [BsonElement("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [Required]
    [BsonElement("type")]
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [BsonElement("options")]
    [JsonPropertyName("options")]
    public List<string>? Options { get; set; }

    [Required]
    [BsonElement("answers")]
    [JsonPropertyName("answers")]
    public List<Answer>? Answers { get; set; }
}
