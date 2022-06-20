using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photogram.Models
{
    public class Notifications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Users? User { get; set; }
        public Photos? Photo { get; set; }
        public string Content { get; set; }

        public Notifications(Users user, Photos photo, string content)
        {
            Date = DateTime.Now;
            User = user;
            Photo = photo;
            Content = content;
        }

        public Notifications()
        {

        }
    }
}
