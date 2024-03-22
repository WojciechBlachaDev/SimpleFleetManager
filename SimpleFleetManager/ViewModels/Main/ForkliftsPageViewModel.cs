using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
using SimpleFleetManager.ViewModels.ForkliftPages;
using SimpleFleetManager.Views.ForkliftPages;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleFleetManager.ViewModels.Main
{
    public class ForkliftsPageViewModel : BaseViewModel
    {
        #region Variables
        private Page? _forkliftsCurrentPage;
        public Page ForkliftsCurrentPage
        {
            get
            {
                return _forkliftsCurrentPage ?? new();
            }
            set
            {
                _forkliftsCurrentPage = value;
                OnPropertyChanged(nameof(ForkliftsCurrentPage));
            }
        }
        private Forklift? _selectedForklift;
        public Forklift SelectedForklift
        {
            get
            {
                return _selectedForklift ?? new();
            }
            set
            {
                if (_selectedForklift != value)
                {
                    _selectedForklift = value;
                    OnPropertyChanged(nameof(SelectedForklift));
                }
            }
        }
        private List<Forklift>? _avaibleForklifts;
        public List<Forklift> AvaibleForklifts
        {
            get
            {
                return _avaibleForklifts ?? [];
            }
            set
            {
                _avaibleForklifts = value;
                OnPropertyChanged(nameof(AvaibleForklifts));
            }
        }
        private bool _isComboboxOpened;
        public bool IsComboboxOpened
        {
            get
            {
                return _isComboboxOpened;
            }
            set
            {
                _isComboboxOpened = value;
                OnPropertyChanged(nameof(IsComboboxOpened));
            }
        }
        private int _currentPageNumber;
        public int CurrentPageNumber
        {
            get
            {
                return _currentPageNumber;
            }
            set
            {
                if (_currentPageNumber != value)
                {
                    _currentPageNumber = value;
                    OnPropertyChanged(nameof(CurrentPageNumber));
                }
            }
        }
        private readonly UserStore? _userStore;
        private readonly ForkliftDataService? _forkliftDataService;
        private readonly LogsDataService _logsDataService;
        #endregion
        #region Constructor
        public ForkliftsPageViewModel(UserStore userStore, ForkliftDataService forkliftDataService, List<Forklift> avaibleForklifts, LogsDataService logsDataService)
        {
            _userStore = userStore;
            _forkliftDataService = forkliftDataService;
            _avaibleForklifts = avaibleForklifts ?? [];
            _logsDataService = logsDataService;
            ActualParametersButtonClick = new RelayCommand(ExecuteActualParametersButtonClick);
            ErrorsPageButtonClick = new RelayCommand(ExecuteErrorsPageButtonClick);
            SafetyPageButtonClick = new RelayCommand(ExecuteSafetyPageButtonClick);
            SickApiPageButtonClick = new RelayCommand(ExecuteSickApiPageButtonClick);
            WorkPageButtonClick = new RelayCommand(ExecuteWorkPageButtonClick);
            LogPageButtonClick = new RelayCommand(ExecuteLogPageButtonClick);
            SelectForkliftFromList = new RelayCommand(ExecuteSelectForkliftFromList);
        }
        #endregion
        #region Logic
        private void RefreshPage(int selectedPage)
        {
            switch (selectedPage)
            {
                case 1:
                    _selectedForklift ??= new();
                    ForkliftsCurrentPage = new ActualParametersPage(new ActualParametersPageVIewModel(_selectedForklift));
                    break;
                case 2:
                    _selectedForklift ??= new();
                    ForkliftsCurrentPage = new ErrorsPage(new ErrorsPageViewModel(_selectedForklift));
                    break;
                case 3:
                    _selectedForklift ??= new();
                    ForkliftsCurrentPage = new SafetyPage(new SafetyPageViewModel(_selectedForklift));
                    break;
                case 4:
                    _selectedForklift ??= new();
                    ForkliftsCurrentPage = new SickApiPage(new SickApiPageViewModel(_selectedForklift));
                    break;
                case 5:
                    _selectedForklift ??= new();
                    ForkliftsCurrentPage = new WorkPage(new WorkPageViewModel(_selectedForklift));
                    break;
                case 6:
                    _selectedForklift ??= new();
                    ForkliftsCurrentPage = new LogPage(new LogsPageViewModel(_selectedForklift, _logsDataService));
                    break;
            }
        }
        #endregion
        #region Button executions
        private void ExecuteActualParametersButtonClick(object? o)
        {
            RefreshPage(1);
        }
        private void ExecuteErrorsPageButtonClick(object o)
        {
            RefreshPage(2);
        }
        private void ExecuteSafetyPageButtonClick(object? o)
        {
            RefreshPage(3);
        }
        private void ExecuteSickApiPageButtonClick(object? o)
        {
            RefreshPage(4);
        }
        private void ExecuteWorkPageButtonClick(object? o)
        {
            RefreshPage(5);
        }
        private void ExecuteLogPageButtonClick(object? o)
        {
            RefreshPage(6);
        }
        private void ExecuteSelectForkliftFromList(object? o)
        {
            if (o != null && _avaibleForklifts != null)
            {
                foreach (Forklift forklift in _avaibleForklifts)
                {
                    if (forklift != null && forklift.Id == Convert.ToInt32(o))
                    {
                        SelectedForklift = forklift;
                        IsComboboxOpened = false;
                        RefreshPage(_currentPageNumber);
                    }
                }
            }
        }
        #endregion
        #region ICommand declarations
        public ICommand? ActualParametersButtonClick { get; private set; }
        public ICommand? ErrorsPageButtonClick { get; private set; }
        public ICommand? SafetyPageButtonClick { get; private set; }
        public ICommand? SickApiPageButtonClick { get; private set; }
        public ICommand? WorkPageButtonClick { get; private set; }
        public ICommand? LogPageButtonClick { get; private set; }
        public ICommand? SelectForkliftFromList { get; private set; }
        #endregion
    }
}
