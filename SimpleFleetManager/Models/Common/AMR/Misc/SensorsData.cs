namespace SimpleFleetManager.Models.Common.AMR.Misc
{
    public class SensorsData
    {
        public double TiltAxis1 { get; set; }
        public double TiltAxis2 { get; set; }
        public double Weight { get; set; }
        public double WeightSaved { get; set; }
        public double ForksHeight { get; set; }
        public double BatteryVoltage { get; set; }
        public double BatteryPercentage { get; set; }
        public double ServoAngle { get; set; }
        public bool ForksHeightLimiter { get; set; }
        public bool ChargingNeed { get; set; }
    }
}
