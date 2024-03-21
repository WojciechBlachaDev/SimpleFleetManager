using SimpleFleetManager.Models.Main;
using SimpleFleetManager.ViewModels.Common;

namespace SimpleFleetManager.ViewModels.ForkliftPages
{
    public class SickApiPageViewModel : BaseViewModel
    {
        private Forklift? _selectedForklift;
        public Forklift SelectedForklift
        {
            get
            {
                return _selectedForklift ?? new();
            }
            set
            {
                _selectedForklift = value;
                OnPropertyChanged(nameof(SelectedForklift));
            }
        }
        public SickApiPageViewModel(Forklift selectedForklift)
        {
            _selectedForklift = selectedForklift;
        }
    }
}
