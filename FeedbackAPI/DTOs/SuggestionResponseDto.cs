namespace FeedbackAPI.DTOs;

public class SuggestionResponseDto
{
    public long id { get; set; }
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public long voteCount { get; set; }     
    public bool isTrending { get; set; }    
    public string username { get; set; } = string.Empty; 
    public DateTime createdAt { get; set; }
}