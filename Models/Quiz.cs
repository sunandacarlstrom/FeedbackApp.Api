using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Quiz
{
    [Required]
    [BsonElement("questions")]
    [JsonPropertyName("questions")]
    public List<Question>? Questions { get; set; }
}