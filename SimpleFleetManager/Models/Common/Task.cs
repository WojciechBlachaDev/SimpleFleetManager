namespace SimpleFleetManager.Models.Common
{
    public class Task
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public Pose Position { get; set; }
        public bool IsRunning { get; set; }
        public bool IsDone { get; set; }
        public bool IsCanceled { get; set; }
        public Task() 
        {
            Position = new();
        }
    }
}
