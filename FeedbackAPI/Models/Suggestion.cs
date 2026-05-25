using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackAPI.Models;


[Table("Suggestions")]
public class Suggestion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id{ get;set;}

    [Required]
    [MaxLength(100)]
    public string title {get; set;} = string.Empty;

    [Required]
    public string description{get; set;} = string.Empty;

    public DateTime createdAt {get;set;} = DateTime.Now;

    [ForeignKey("User")] 
    public long userId { get; set; }

    public User? user { get; set; }

    public virtual List<Vote> votes {get; set;} = new List<Vote>();
}