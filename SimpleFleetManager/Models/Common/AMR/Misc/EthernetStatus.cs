namespace SimpleFleetManager.Models.Common.AMR.Misc
{
    public class EthernetStatus
    {
        public bool LidarLoc { get; set; }
        public bool Plc { get; set; }
        public bool Visionary { get; set; }
        public bool SafetyModbus { get; set; }
        public bool GatewayLan { get; set; }
        public bool LidarLeft { get; set; }
        public bool LidarRight { get; set; }
        public bool SafetyEthernet { get; set; }
        public bool GatewayWiFi { get; set; }
        public bool Server { get; set; }
    }
}
