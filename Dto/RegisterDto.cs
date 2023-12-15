using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.Api.Dto;

public class RegisterDto
{
    [Required(ErrorMessage = "User Name is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Permission is required")]
    public string? Permission { get; set; }
}