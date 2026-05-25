using System.ComponentModel.DataAnnotations;

namespace FeedbackAPI.DTOs;

public class RegisterUserDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string password { get; set; } = string.Empty;
}