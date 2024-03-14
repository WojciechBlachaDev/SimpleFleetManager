﻿using SimpleFleetManager.Models.Common.AMR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace SimpleFleetManager.Models.Main
{
    public class Forklift
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string ForkliftIpAddress { get; set; }
        public string? LidarLocIpAddress { get; set; }
        public string? VisionaryIpAddress { get; set; }
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
