using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFleetManager.Models.Main
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PriorityLevel { get; set; }
        public string? Name { get; set; }
        public bool IsQueued { get; set; }
        public bool IsRunning { get; set; }
        public bool IsDone { get; set; }
        public bool IsCanceled { get; set; }
        public int CurrentJobStep { get; set; }
        public List<JobStep>? JobSteps { get; set; }
        public Job()
        {
            Name = string.Empty;
            JobSteps = [];
        }
    }
}
