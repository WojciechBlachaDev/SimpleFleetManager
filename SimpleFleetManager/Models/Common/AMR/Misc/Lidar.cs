namespace SimpleFleetManager.Models.Common.AMR.Misc
{
    public class Lidar
    {
        public bool IsActive { get; set; }
        public bool ContaminationError { get; set; }
        public bool ContaminationWarning { get; set; }
        public bool DeviceError { get; set; }
        public bool AppError { get; set; }
        public bool SwitchMonitoringCaseError { get; set; }
        public bool ReducedSpeedZoneStatus { get; set; }
        public bool SoftStopZoneStatus { get; set; }
        public bool EmergencyStopZoneStatus { get; set; }
    }
}
