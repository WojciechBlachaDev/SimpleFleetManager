using SimpleFleetManager.Models.Common.AMR;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.ViewModels.Common;
namespace SimpleFleetManager.ViewModels.ForkliftPages
{
    public class ActualParametersPageVIewModel : BaseViewModel
    {
        #region Variables
        private Forklift? _selectedForklift;
        public Forklift? SelectedForklift
        {
            get { return _selectedForklift; }
            set { if (_selectedForklift != value) { _selectedForklift = value; OnPropertyChanged(nameof(SelectedForklift)); } }
        }
        private DataOut? _data;
        public DataOut Data
        {
            get { return _data ?? new(); ; }
            set { _data = value; OnPropertyChanged(nameof(Data)); }
        }
        private DateTime _refreshDate;
        public DateTime RefreshDate
        {
            get { return _refreshDate; }
            set { _refreshDate = value; OnPropertyChanged(nameof(RefreshDate)); }
        }
        #endregion
        #region Cosntructor
        public ActualParametersPageVIewModel(Forklift selectedForklift)
        {
            SelectedForklift = selectedForklift;
            Task.Run(() => { RefreshData(); });
        }
        #endregion
        private async void RefreshData()
        {
            while (_selectedForklift != null && _selectedForklift.Client != null && _selectedForklift.Client.Connected && _selectedForklift.DataOut != null) 
            {
                Data = _selectedForklift.DataOut;
                RefreshDate = DateTime.Now;
                await Task.Delay(30);
                if (!_selectedForklift.Client.Connected) {  break; }
            }  
        }     
    }
}
