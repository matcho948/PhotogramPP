using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photogram.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool IsBanned { get; set; }
        public virtual List<Photos>? Photos { get; set; }
        [Required]
        public UserType Role { get; set; }
        public enum UserType
        {
            User, Administrator
        }
        public Users(string name,string password,string email)
        {
            Name = name;
            Password = password;
            Email = email;
            IsBanned = false;
        }
    }
}
