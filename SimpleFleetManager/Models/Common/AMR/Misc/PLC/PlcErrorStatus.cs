namespace SimpleFleetManager.Models.Common.AMR.Misc.PLC
{
    public class PlcErrorStatus
    {
        public bool PressureSensor { get; set; }
        public bool ForksHeightSensor { get; set; }
        public bool TiltSensorAxis1 { get; set; }
        public bool TiltSensorAxis2 { get; set; }
        public bool BatteryVoltageRead { get; set; }
        public bool ManualModeSpeedRegulator { get; set; }
        public bool PowerToCurtisWrite { get; set; }
        public bool ServoPositionRead { get; set; }
        public bool ServoMove { get; set; }
        public bool ServoHalt { get; set; }
        public bool ScangridLeftReadMeasurement { get; set; }
        public bool ScangridRightReadMeasurement { get; set; }
    }
}
