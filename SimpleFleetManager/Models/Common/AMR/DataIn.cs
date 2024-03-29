using SimpleFleetManager.Models.Common.AMR.Misc;
namespace SimpleFleetManager.Models.Common.AMR
{
    public class DataIn
    {
        public Commands? Commands { get; set; }
        public Task? Task { get; set; }
        public DataIn()
        {
            Commands = new();
            Task = new();
        }
    }
}
