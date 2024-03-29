using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SimpleFleetManager.Models.Common.AMR.Misc
{
    public class ForkliftLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ForkliftId { get; set; }
        public string? Date { get; set; }
        public int Level { get; set; }
        public string? Node { get; set; }
        public string? Message { get; set; }
        public string? File { get; set; }
        public int CodeLine { get; set; }
    }
}
