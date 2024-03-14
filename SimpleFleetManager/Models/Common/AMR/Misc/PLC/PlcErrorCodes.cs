namespace SimpleFleetManager.Models.Common.AMR.Misc.PLC
{
    public class PlcErrorCodes
    {
        public int PressureSensor { get; set; }
        public int ForksHeightSensor { get; set; }
        public int TiltSensorAxis1 { get; set; }
        public int TiltSensorAxis2 { get; set; }
        public int BatteryVoltageRead { get; set; }
        public int ManualModeSpeedRegulator { get; set; }
        public int PowerToCurtisWrite { get; set; }
        public int ServoPositionRead { get; set; }
        public int ServoMove { get; set; }
        public int ServoHalt { get; set; }
        public int ScangridLeftReadMeasurement { get; set; }
        public int ScangridRightReadMeasurement { get; set; }
    }
}
