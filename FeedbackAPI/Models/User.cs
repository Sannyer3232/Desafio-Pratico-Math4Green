using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackAPI.Models;

[Table("Users")]
public class User
{
    [Key] //Definindo esse atributo como Chave Primaria
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Definindo como auto incremento
    public long id
    {
        get;
        set;
    }

    [Required] //Definindo como não nulo
    [MaxLength(255)] 
    public String username
    {
        get;
        set;
    } = string.Empty;

    [Required] 
    public string password { get; set; } = string.Empty;
}