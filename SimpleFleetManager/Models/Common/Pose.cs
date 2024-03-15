using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFleetManager.Models.Common
{
    public class Pose
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double R { get; set; }
    }
}
