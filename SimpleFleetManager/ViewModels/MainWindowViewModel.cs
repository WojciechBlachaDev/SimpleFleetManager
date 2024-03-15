using SimpleFleetManager.ViewModels.Common;
using System.Windows.Controls;

namespace SimpleFleetManager.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Variables
        #region Page variables
        private Page? _currentPage;
        public Page CurrentPage
        {
            get
            {
                return _currentPage ??= new();
            }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }
        private bool _showMenu;
        public bool ShowMenu
        {
            get
            {
                return _showMenu;
            }
            set
            {
                if (_showMenu != value)
                {
                    _showMenu = value;
                    OnPropertyChanged(nameof(ShowMenu));
                }
            }
        }

        #endregion
        #endregion
        #region Constructors
        public MainWindowViewModel()
        {
            ShowMenu = true;
        }
        #endregion
    }
}
