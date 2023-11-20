using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedbackApp.Api.Models;

public class Question
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Text { get; set; }
    public List<Answer>? Answers { get; set; }
    // TODO: Fråga beställaren om man vill spara användar-ID för svaret
    public int? UserId { get; set; }
}
