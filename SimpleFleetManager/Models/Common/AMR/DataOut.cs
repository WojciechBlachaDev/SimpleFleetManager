using SimpleFleetManager.Models.Common.AMR.Misc;
using SimpleFleetManager.Models.Common.AMR.Misc.Config;
using SimpleFleetManager.Models.Common.AMR.Misc.PLC;
namespace SimpleFleetManager.Models.Common.AMR
{
    public class DataOut
    {
        public Pose? ActualPosition { get; set; }
        public SensorsData? Sensors { get; set; }
        public SafetyStatus? Safety { get; set; }
        public PlcErrorStatus? PlcErrorStatus { get; set; }
        public PlcErrorCodes? PlcErrorCodes { get; set; }
        public EncoderData? Encoder { get; set; }
        public Scangrid? ScangridLeft { get; set; }
        public Scangrid? ScangridRight { get; set; }
        public Workstates? ActualWorkstates { get; set; }
        public EthernetStatus? Ethernet { get; set; }
        public TebConfig? ActualTebConfig { get; set; }
        public Task? ActualTask { get; set; }
        public DataOut()
        {
            ActualPosition = new();
            Sensors = new();
            Safety = new();
            PlcErrorStatus = new();
            PlcErrorCodes = new();
            Encoder = new();
            ScangridLeft = new();
            ScangridRight = new();
            ActualWorkstates = new();
            Ethernet = new();
            ActualTask = new();
        }
    }
}
