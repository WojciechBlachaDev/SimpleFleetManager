namespace SimpleFleetManager.Models.Common.AMR.Misc
{
    public class SafetyStatus
    {
        public bool CpuOk { get; set; }
        public bool EncoderOk { get; set; }
        public bool SafeSpeed { get; set; }
        public bool Standstill { get; set; }
        public bool ButtonemergencyLeft { get; set; }
        public bool ButtonemergencyRight { get; set; }
        public Lidar? LidarLeft { get; set; }
        public Lidar? LidarRight { get; set; }
    }
}
