namespace SimpleFleetManager.Models.Common.AMR.Misc.Config
{
    public class TebConfig
    {
        public double MaxVelForward { get; set; }
        public double MaxVelBackward { get; set; }
        public double MaxVelAngular { get; set; }
        public double MaxAccForwardBackward { get; set; }
        public double MaxAccAngular { get; set; }
        public double Wheelbase { get; set; }
        public double GoalLinearTolerance { get; set; }
        public double GoalAngularTolerance { get; set; }
        public double MinObstacleDistance { get; set; }
        public double ObstacleInflationRadius { get; set; }
        public double DynamicObstacleInflationRadius { get; set; }
        public double DtRef { get; set; }
        public double DtHysteresis { get; set; }
        public bool IncludeDynamicObstacles { get; set; }
        public bool IncludeCostmapObstacles { get; set; }
        public bool OscillationRecovery { get; set; }
        public bool AllowInitWithBackwardMotion { get; set; }
        public bool Save { get; set; }
    }
}
