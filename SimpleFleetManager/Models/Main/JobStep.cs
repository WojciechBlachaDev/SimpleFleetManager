﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFleetManager.Models.Main
{
    internal class JobStep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Location Location { get; set; }
        [Required]
        public int Type { get; set; }
        public bool IsRunning { get; set; }
        public bool IsDone { get; set; }
        public bool IsCanceled { get; set; }
        public JobStep()
        {
            Location = new();
        }
    }
}