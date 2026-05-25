using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace FeedbackAPI.Models;

[Table("Votes")]
public class Vote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id{get; set;}

    public DateTime voteAt {get; set;} = DateTime.Now;

    [ForeignKey("User")]
    public long userId {get; set;}
    public User? user {get; set;}

    [ForeignKey("Suggestion")]
    public long suggestionId {get; set;}
    public Suggestion ? suggestion {get; set;}
}