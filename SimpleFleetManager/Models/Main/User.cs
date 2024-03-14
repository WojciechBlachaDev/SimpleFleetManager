using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFleetManager.Models.Main
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string? CardTag { get; set; }
        public bool IsLogged { get; set; }
        public int AccessLevel { get; set; }
        public User()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
