using SimpleFleetManager.Models.Common.AMR.Misc;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
using System.Windows.Input;

namespace SimpleFleetManager.ViewModels.ForkliftPages
{
    public class LogsPageViewModel : BaseViewModel
    {
        #region Variables
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
        private ForkliftLog? _selectedForkliftLog;
        public ForkliftLog SelectedForkliftLog
        {
            get
            {
                return _selectedForkliftLog ?? new();
            }
            set
            {
                _selectedForkliftLog = value;
                OnPropertyChanged(nameof(SelectedForkliftLog));
            }
        }
        private IEnumerable<ForkliftLog>? _forkliftDatabaseLogs;
        public IEnumerable<ForkliftLog> ForkliftDatabaseLogs
        {
            get
            {
                return _forkliftDatabaseLogs ?? [];
            }
            set
            {
                _forkliftDatabaseLogs = value;
                OnPropertyChanged(nameof(ForkliftDatabaseLogs));
            }
        }
        private List<ForkliftLog>? _selectedForkliftSavedLogs;
        public List<ForkliftLog> SelectedForkliftSavedLogs
        {
            get
            {
                return _selectedForkliftSavedLogs ?? [];
            }
            set
            {
                _selectedForkliftSavedLogs = value;
                OnPropertyChanged(nameof(SelectedForkliftSavedLogs));
            }
        }
        private List<ForkliftLog>? _filteredForkliftSavedLogs;
        public List<ForkliftLog> FilteredForkliftSavedLogs
        {
            get
            {
                return _filteredForkliftSavedLogs ??= [];
            }
            set
            {
                _filteredForkliftSavedLogs = value;
                OnPropertyChanged(nameof(FilteredForkliftSavedLogs));
            }
        }
        private ForkliftLog? _filteredActualLog;
        public ForkliftLog FilteredActualLog
        {
            get
            {
                return _filteredActualLog ??= new();
            }
            set
            {
                _filteredActualLog = value;
                OnPropertyChanged(nameof(FilteredActualLog));
            }
        }
        private readonly LogsDataService _logsDataService;
        private int _minimumActualLogLevel = 1;
        public int MinimumActualLogLevel
        {
            get
            {
                return _minimumActualLogLevel;
            }
            set
            {
                _minimumActualLogLevel = value;
                OnPropertyChanged(nameof(MinimumActualLogLevel));
            }
        }
        private int _maximumActualLogLevel = 5;
        public int MaximumActualLogLevel
        {
            get
            {
                return _maximumActualLogLevel;
            }
            set
            {
                _maximumActualLogLevel = value;
                OnPropertyChanged(nameof(MaximumActualLogLevel));
            }
        }
        private int _minimumSavedLogLevel = 1;
        public int MinimumSavedLogLevel
        {
            get
            {
                return _minimumSavedLogLevel;
            }
            set
            {
                _minimumSavedLogLevel = value;
                OnPropertyChanged(nameof(MinimumSavedLogLevel));
            }
        }
        private int _maximumSavedlLogLevel = 5;
        public int MaximumSavedLogLevel
        {
            get
            {
                return _maximumSavedlLogLevel;
            }
            set
            {
                _maximumSavedlLogLevel = value;
                OnPropertyChanged(nameof(MaximumSavedLogLevel));
            }
        }
        private List<string>? _avaibleLevels;
        public List<string>? AvaibleLevels
        {
            get
            {
                return _avaibleLevels;
            }
            set
            {
                _avaibleLevels = value;
                OnPropertyChanged(nameof(AvaibleLevels));
            }
        }
        private bool _minimumActualLogBoxOpened;
        public bool MinimumActualLogBoxOpened
        {
            get
            {
                return _minimumActualLogBoxOpened;
            }
            set
            {
                _minimumActualLogBoxOpened = value;
                OnPropertyChanged(nameof(MinimumActualLogBoxOpened));
            }
        }
        private bool _maximumActualLogBoxOpened;
        public bool MaximumActualLogBoxOpened
        {
            get
            {
                return _maximumActualLogBoxOpened;
            }
            set
            {
                _maximumActualLogBoxOpened = value;
                OnPropertyChanged(nameof(MaximumActualLogBoxOpened));
            }
        }
        private bool _minimumSavedLogBoxOpened;
        public bool MinimumSavedLogBoxOpened
        {
            get
            {
                return _minimumSavedLogBoxOpened;
            }
            set
            {
                _minimumSavedLogBoxOpened = value;
                OnPropertyChanged(nameof(MinimumSavedLogBoxOpened));
            }
        }
        private bool _maximumSavedLogBoxOpened;
        public bool MaximumSavedLogBoxOpened
        {
            get
            {
                return _maximumSavedLogBoxOpened;
            }
            set
            {
                _maximumSavedLogBoxOpened = value;
                OnPropertyChanged(nameof(MaximumSavedLogBoxOpened));
            }
        }
        #endregion
        #region Constructor
        public LogsPageViewModel(Forklift selectedForklift,  LogsDataService logsDataService)
        {
            _logsDataService = logsDataService;
            _selectedForklift = selectedForklift;
            AvaibleLevels = new List<string> { "DEBUG", "INFO", "WARNING", "ERROR", "CRITICAL" };
            SelectMaximumActualLogLevel = new RelayCommand(ExecuteSelectMaximumActualLogLevel);
            SelectMinimumActualLogLevel = new RelayCommand(ExecuteSelectMinimumActualLogLevel);
            SelectMaximumSavedLogLevel = new RelayCommand(ExecuteSelectMaximumSavedLogLevel);
            SelectMinimumSavedLogLevel = new RelayCommand(ExecuteSelectMinimumSavedLogLevel);
            Task.Run(() => { RefreshData(); });
        }
        #endregion
        #region Logic
        private async void RefreshData()
        {
            while (_selectedForklift != null && _selectedForklift.ActualLog != null)
            {
                SelectedForkliftLog = _selectedForklift.ActualLog;
                ForkliftDatabaseLogs = await _logsDataService.GetAll();
                SelectedForkliftSavedLogs.Clear();
                foreach (ForkliftLog log in ForkliftDatabaseLogs)
                {
                    if (log.ForkliftId == _selectedForklift.Id)
                    {
                        SelectedForkliftSavedLogs.Append(log);
                    }
                }
                FilterActualLogs();
                FilterSavedLogs();
                
            }
        }
        #endregion
        #region Logic
        private bool IsLogLevelOk(int level)
        {
            if (1 > level) { return false; }
            if (5 < level) { return false; } 
            return true;
        }
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
                    FilteredForkliftSavedLogs.Append(log);
                }
            }
        }
        #endregion
        #region Buttons execution
        private void ExecuteSelectMinimumActualLogLevel(object? o)
        {
            if (o != null && IsLogLevelOk(Convert.ToInt32(o)))
            {
                MinimumActualLogLevel = (int)o;
                if (MinimumActualLogLevel > MaximumActualLogLevel)
                {
                    MaximumActualLogLevel = MinimumActualLogLevel;
                }
            }
            MinimumActualLogBoxOpened = false;
        }
        private void ExecuteSelectMaximumActualLogLevel(object? o)
        {
            if(o != null && IsLogLevelOk(Convert.ToInt32(o)))
            {
                MaximumActualLogLevel = (int)o;
                if (MaximumActualLogLevel < MinimumActualLogLevel)
                {
                    MinimumActualLogLevel = MaximumActualLogLevel;
                }
            }
            MaximumSavedLogBoxOpened = false;
        }
        private void ExecuteSelectMinimumSavedLogLevel(object? o)
        {
            if( o != null && IsLogLevelOk(Convert.ToInt32(o)))
            {
                MinimumSavedLogLevel = (int)o;
                if (MinimumSavedLogLevel > MaximumSavedLogLevel)
                {
                    MaximumSavedLogLevel = MinimumSavedLogLevel;
                }
            }
            MinimumSavedLogBoxOpened = false;
        }
        private void ExecuteSelectMaximumSavedLogLevel(object? o)
        {
            if (o != null && IsLogLevelOk(Convert.ToInt32(o))) 
            { 
                MaximumSavedLogLevel = (int)o; 
                if (MaximumSavedLogLevel < MinimumSavedLogLevel)
                {
                    MinimumSavedLogLevel = MaximumSavedLogLevel;
                }
            }
            MaximumSavedLogBoxOpened = false;
        }
        #endregion
        #region ICommand declarations
        public ICommand? SelectMinimumActualLogLevel {  get; private set; }
        public ICommand? SelectMaximumActualLogLevel { get; private set; }
        public ICommand? SelectMinimumSavedLogLevel { get; private set; }
        public ICommand? SelectMaximumSavedLogLevel { get; private set; }
        #endregion
    }
}
