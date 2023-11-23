
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;
public class CompanyPermissions
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

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