using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Photogram.Models
{
    public class Photos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string PhotoUrl { get; set; }

        public string Description { get; set; }
        public bool IsMainPhoto { get; set; }
        public virtual List<Comments>? Comments { get; set; }
        public virtual List<Reactions>? Reactions { get; set; }
        public Photos(string url, string description)
        {
            PhotoUrl = url;
            Description = description;
            IsMainPhoto = false;
        }
        public Photos()
        {

        }
    }
}
