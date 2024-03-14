namespace SimpleFleetManager.Models.Common.AMR.Misc
{
    public class Commands
    {
        public bool ManualModeOverride { get; set; }
        public bool StartTask { get; set; }
        public bool PauseTask { get; set; }
        public bool ContinueTask { get; set; }
        public bool CancelTask { get; set; }
        public bool EmergencyStop { get; set; }
    }
}
