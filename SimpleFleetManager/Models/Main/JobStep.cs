﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SimpleFleetManager.Models.Main
{
    public class JobStep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double LocX { get; set; }
        public double LocY { get; set; }
        public double LocR { get; set; }
        [Required]
        public int Type { get; set; }
        public bool IsRunning { get; set; }
        public bool IsDone { get; set; }
        public bool IsCanceled { get; set; }
        public int JobId { get; set; }
        public int LocationId { get; set; }
        public bool IsAssigned {  get; set; }
        public JobStep()
        {
        }
    }
}
