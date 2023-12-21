using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace FeedbackApp.Api.Models;

[CollectionName("roles")]
public class Role : MongoIdentityRole<ObjectId>
{
    public Role() { }
    public Role(string roleName) : base(roleName) { }
}
