namespace FeedbackApp.Api.Models;

public class MongoDBSettings
{
    public string ConnectionURI {get; set; } = null!; 
    public string DatabaseName {get; set; } = null!; 
    public string UserCollection {get; set; } = null!; 
    public string CompanyCollection {get; set; } = null!; 
    public string EventCollection {get; set; } = null!; 
}