using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Answer
{
    [Required]
    [BsonElement("session")]
    [JsonPropertyName("session")]
    public Guid Session { get; set; } = Guid.NewGuid();

    [Required]
    [BsonElement("result")]
    [JsonPropertyName("result")]
    public List<string>? Result { get; set; }
}
