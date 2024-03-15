using SimpleFleetManager.Models.Common.AMR.Misc.Scangrids;


namespace SimpleFleetManager.Models.Common.AMR.Misc
{
    public class Scangrid
    {
        public List<int> Ranges { get; set; }
        public ScangridStatus? Status { get; set; }
        public Scangrid() 
        {
            Ranges = [];
        }

    }
}
