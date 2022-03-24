using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Photogram.Models
{
    public class Reactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Type TypeOfReaction { get; set; }
        public enum Type
        {
            Like,Love,Sad,Excited
        }
    }
}
