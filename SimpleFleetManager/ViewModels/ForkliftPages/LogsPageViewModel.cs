using SimpleFleetManager.Models.Common.AMR.Misc;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
namespace SimpleFleetManager.ViewModels.ForkliftPages
{
    public class LogsPageViewModel : BaseViewModel
    {
        #region Variables
        private Forklift? _selectedForklift;
        public Forklift SelectedForklift
        {
            get { return _selectedForklift ?? new(); }
            set { _selectedForklift = value; OnPropertyChanged(nameof(SelectedForklift)); }
        }
        private ForkliftLog? _selectedForkliftLog;
        public ForkliftLog SelectedForkliftLog
        {
            get { return _selectedForkliftLog ?? new(); }
            set { _selectedForkliftLog = value; OnPropertyChanged(nameof(SelectedForkliftLog)); }
        }
        private IEnumerable<ForkliftLog>? _forkliftDatabaseLogs;
        public IEnumerable<ForkliftLog> ForkliftDatabaseLogs
        {
            get { return _forkliftDatabaseLogs ?? []; }
            set { _forkliftDatabaseLogs = value; OnPropertyChanged(nameof(ForkliftDatabaseLogs)); }
        }
        private List<ForkliftLog>? _selectedForkliftSavedLogs;
        public List<ForkliftLog> SelectedForkliftSavedLogs
        {
            get { return _selectedForkliftSavedLogs ?? []; }
            set { _selectedForkliftSavedLogs = value; OnPropertyChanged(nameof(SelectedForkliftSavedLogs)); }
        }
        private List<ForkliftLog>? _filteredForkliftSavedLogs;
        public List<ForkliftLog> FilteredForkliftSavedLogs
        {
            get { return _filteredForkliftSavedLogs ??= []; }
            set {  _filteredForkliftSavedLogs = value;  OnPropertyChanged(nameof(FilteredForkliftSavedLogs)); }
        }
        private ForkliftLog? _filteredActualLog;
        public ForkliftLog FilteredActualLog
        {
            get { return _filteredActualLog ??= new(); }
            set { _filteredActualLog = value; OnPropertyChanged(nameof(FilteredActualLog)); }
        }
        private readonly LogsDataService _logsDataService;
        private int _minimumActualLogLevel;
        public int MinimumActualLogLevel
        {
            get { return _minimumActualLogLevel; }
            set { if (_minimumActualLogLevel != value) { _minimumActualLogLevel = value; VerifyLogLevel(1); OnPropertyChanged(nameof(MinimumActualLogLevel)); } }
        }
        private int _maximumActualLogLevel;
        public int MaximumActualLogLevel
        {
            get { return _maximumActualLogLevel; }
            set { if (_maximumActualLogLevel != value) { _maximumActualLogLevel = value; VerifyLogLevel(2); OnPropertyChanged(nameof(MaximumActualLogLevel)); } }
        }
        private int _minimumSavedLogLevel;
        public int MinimumSavedLogLevel
        {
            get { return _minimumSavedLogLevel; }
            set { if (_minimumSavedLogLevel != value) { _minimumSavedLogLevel = value; VerifyLogLevel(1); OnPropertyChanged(nameof(MinimumSavedLogLevel)); } }
        }
        private int _maximumSavedlLogLevel;
        public int MaximumSavedLogLevel
        {
            get { return _maximumSavedlLogLevel; }
            set { if (_maximumSavedlLogLevel != value) { _maximumSavedlLogLevel = value; VerifyLogLevel(2); OnPropertyChanged(nameof(MaximumSavedLogLevel)); } }
        }
        private List<string>? _avaibleLevels;
        public List<string>? AvaibleLevels
        {
            get { return _avaibleLevels; }
            set { _avaibleLevels = value; OnPropertyChanged(nameof(AvaibleLevels)); }
        }
        private bool _minimumActualLogBoxOpened;
        public bool MinimumActualLogBoxOpened
        {
            get { return _minimumActualLogBoxOpened; }
            set { if (_minimumActualLogBoxOpened != value) { _minimumActualLogBoxOpened = value; OnPropertyChanged(nameof(MinimumActualLogBoxOpened)); } }
        }
        private bool _maximumActualLogBoxOpened;
        public bool MaximumActualLogBoxOpened
        {
            get { return _maximumActualLogBoxOpened; }
            set { if (_maximumActualLogBoxOpened != value) { _maximumActualLogBoxOpened = value; OnPropertyChanged(nameof(MaximumActualLogBoxOpened)); } }
        }
        private bool _minimumSavedLogBoxOpened;
        public bool MinimumSavedLogBoxOpened
        {
            get { return _minimumSavedLogBoxOpened; }
            set { _minimumSavedLogBoxOpened = value; OnPropertyChanged(nameof(MinimumSavedLogBoxOpened)); }
        }
        private bool _maximumSavedLogBoxOpened;
        public bool MaximumSavedLogBoxOpened
        {
            get { return _maximumSavedLogBoxOpened; }
            set { _maximumSavedLogBoxOpened = value; OnPropertyChanged(nameof(MaximumSavedLogBoxOpened)); }
        }
        #endregion
        #region Constructor
        public LogsPageViewModel(Forklift selectedForklift, LogsDataService logsDataService)
        {
            _logsDataService = logsDataService;
            _selectedForklift = selectedForklift;
            AvaibleLevels = ["DEBUG", "INFO", "WARNING", "ERROR", "CRITICAL"];
            SetDeafultFilter();
            Task.Run(() => { RefreshData(); });
        }
        #endregion
        #region Logic async
        private async void RefreshData()
        {
            while (_selectedForklift != null && _selectedForklift.ActualLog != null)
            {
                SelectedForkliftLog = _selectedForklift.ActualLog;
                ForkliftDatabaseLogs = await _logsDataService.GetAll();
                SelectedForkliftSavedLogs.Clear();
                foreach (ForkliftLog log in ForkliftDatabaseLogs)
                {
                    if (log.ForkliftId == _selectedForklift.Id) { SelectedForkliftSavedLogs.Add(log); }
                }
                FilterActualLogs();
                FilterSavedLogs();
            }
        }
        #endregion
        #region Logic
        private void FilterActualLogs()
        {
            if (_selectedForkliftLog != null && SelectedForkliftLog.Level >= MinimumActualLogLevel && SelectedForkliftLog.Level <= MaximumActualLogLevel)
            {
                FilteredActualLog = _selectedForkliftLog;
            }
        }
        private void FilterSavedLogs()
        {
            FilteredForkliftSavedLogs.Clear();
            foreach (ForkliftLog log in SelectedForkliftSavedLogs)
            {
                if (log.Level >= MinimumSavedLogLevel && log.Level <= MaximumActualLogLevel)
                {
                    FilteredForkliftSavedLogs.Add(log);
                }
            }
        }
        private void SetDeafultFilter()
        {
            MinimumActualLogLevel = 1;
            MaximumActualLogLevel = 5;
            MinimumSavedLogLevel = 1;
            MaximumSavedLogLevel = 5;
        }
        private void VerifyLogLevel(int checkType)
        {
            if (checkType == 1)
            {
                if (MinimumSavedLogLevel > MaximumSavedLogLevel) { MaximumSavedLogLevel = MinimumSavedLogLevel; }
                if (MinimumActualLogLevel > MaximumActualLogLevel) { MaximumActualLogLevel = MinimumActualLogLevel; }
            }
            else
            {
                if (MaximumSavedLogLevel < MinimumSavedLogLevel) { MinimumSavedLogLevel = MaximumSavedLogLevel; }
                if (MaximumActualLogLevel < MinimumActualLogLevel) { MinimumActualLogLevel = MaximumActualLogLevel; }
            }
        }
        #endregion
    }
}
