using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Answer
{
    [BsonElement("result")]
    [JsonPropertyName("result")]
    public List<string>? Result { get; set; }
}
