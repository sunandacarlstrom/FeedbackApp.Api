using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.Api.Dto;

public class RegisterUserDto
{
    [Required(ErrorMessage = "First Name is required")]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [PasswordPropertyText]
    public string Password { get; set; }
}