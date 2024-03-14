namespace SimpleFleetManager.Models.Common.AMR.Misc.Scangrids
{
    public class ScangridStatus
    {
        public bool WorkStatus { get; set; }
        public bool SleeepModeStatus { get; set; }
        public bool VoltageError { get; set; }
        public bool ExternalLightResistanceError { get; set; }
        public bool MonitoringCaseSwitchInputValid { get; set; }
        public bool MonitoringCaseSwitchCanValid { get; set; }
        public bool ContaminationWarning { get; set; }
        public bool ContaminationError { get; set; }
        public bool SafetyOutputSignal { get; set; }
        public bool WarningFieldStatus { get; set; }
        public bool ProtectionFieldStatus { get; set; }
    }
}
