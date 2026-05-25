using System.ComponentModel.DataAnnotations;

namespace FeedbackAPI.DTOs;

public class CreateSuggetionsDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O título não pode ter mais de 100 caracteres.")]
    public string title { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string description { get; set; } = string.Empty;

    [Required]
    public long userId { get; set; } 
}