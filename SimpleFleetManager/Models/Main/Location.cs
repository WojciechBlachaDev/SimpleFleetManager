using SimpleFleetManager.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFleetManager.Models.Main
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Type { get; set; }
        public string? Description { get; set; }
        [Required]
        public Pose Position { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public Location()
        {
            Name = string.Empty;
            Position = new();
        }
    }
}
