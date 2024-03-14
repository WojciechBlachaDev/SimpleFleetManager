using SimpleFleetManager.Models.Common.AMR.Misc.Scangrids;


namespace SimpleFleetManager.Models.Common.AMR.Misc
{
    public class Scangrid
    {
        public List<ScangridRange> Ranges { get; set; }
        public ScangridStatus? Status { get; set; }
        public Scangrid()
        {
            Ranges = [];
            for (int i = 0; i < 32; i++)
            {
                Ranges.Add(new ScangridRange { Id = i, Value = 0 });
            }
            Status = new();
        }
    }
}
