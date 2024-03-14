using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFleetManager.Models.Common.AMR
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
        public bool emergencyStopZoneStatus { get; set; }
    }
}
