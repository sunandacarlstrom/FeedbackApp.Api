using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Company
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Required]
    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [BsonElement("business_area")]
    [JsonPropertyName("business_area")]
    public string? BusinessArea { get; set; }

    [BsonElement("full_name")]
    [JsonPropertyName("full_name")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? FullName { get; set; }
}