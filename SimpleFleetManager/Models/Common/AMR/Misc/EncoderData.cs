namespace SimpleFleetManager.Models.Common.AMR.Misc
{
    public class EncoderData
    {
        public double Speed { get; set; }
        public double DistanceKilometers { get; set; }
        public double DistanceMeters { get; set; }
        public int DistanceMiliMeters { get; set; }
        public int CurrentPosition { get; set; }
        public bool IsStandstill { get; set; }
        public bool IsMovingForward { get; set; }
        public bool IsMovingBackward { get; set; }
    }
}
