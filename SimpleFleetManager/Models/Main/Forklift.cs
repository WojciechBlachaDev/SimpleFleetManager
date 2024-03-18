using SimpleFleetManager.Models.Common.AMR;
using SimpleFleetManager.Models.Common.AMR.Misc.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace SimpleFleetManager.Models.Main
{
    public class Forklift
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ForkliftIpAddress { get; set; }
        public string? LidarLocIpAddress { get; set; }
        public string? VisionaryIpAddress { get; set; }
        public DateTime Registrationdate { get; set; }
        public TebConfig? BackedUpTebConfig { get; set; }
        [Required]
        public int Port { get; set; }
        [NotMapped]
        public DataOut? DataOut { get; set; }
        [NotMapped]
        public DataIn? DataIn { get; set; }
        [NotMapped]
        public TcpClient? Client { get; set; }
        [NotMapped]
        public bool IsConnected { get; set; }
        public Forklift()
        {
            Client = new();
            DataIn = new();
            DataOut = new();
        }

    }
}
