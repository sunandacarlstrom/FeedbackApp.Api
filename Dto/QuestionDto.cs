namespace FeedbackApp.Api.Dto;

public class QuestionDto
{
    public string? Title { get; set; }
    public string? Type { get; set; }
    public List<string>? Options { get; set; }
    public int CurrentQuestionIndex {get; set; }
    public int TotalAmountOfQuestions {get; set; }
}