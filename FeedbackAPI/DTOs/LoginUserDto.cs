using System.ComponentModel.DataAnnotations;

namespace FeedbackAPI.DTOs;

public class LoginUserDto
{
    [Required]
    public string username { get; set; } = string.Empty;

    [Required]
    public string password { get; set; } = string.Empty;
}