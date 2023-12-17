using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace FeedbackApp.Api.Models;

[CollectionName("users")]
public class User : MongoIdentityUser<ObjectId>
{
    [BsonElement("company_roles")]
    [JsonPropertyName("company_roles")]
    public List<CompanyRole>? CompanyRoles { get; set; }
}
