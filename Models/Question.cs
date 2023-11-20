using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Question
{
    [BsonElement("question")]
    [JsonPropertyName("question")]
    public string? Text { get; set; }

    [BsonElement("options")]
    [JsonPropertyName("options")]
    public List<string>? Options { get; set; }

    [BsonElement("answers")]
    [JsonPropertyName("answers")]
    public List<Answer>? Answers { get; set; }

    // // TODO: Fråga beställaren om man vill spara användar-ID för svaret
    // public int? UserId { get; set; }
}
