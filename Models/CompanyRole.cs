
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;
public class CompanyRole
{
    [BsonElement("company_id")]
    [JsonPropertyName("company_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CompanyId { get; set; }

    [BsonElement("company_full_name")]
    [JsonPropertyName("company_full_name")]
    public string? CompanyName { get; set; }

    [BsonElement("permission")]
    [JsonPropertyName("permission")]
    public string? Permission { get; set; }
}