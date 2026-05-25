using System.ComponentModel.DataAnnotations;

namespace FeedbackAPI.DTOs;

public class CreateVoteDto
{
    [Required]
    public long suggestionId { get; set; } 

    [Required]
    public long userId { get; set; } 
}