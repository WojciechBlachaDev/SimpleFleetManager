using Serilog;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Communication;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
using System.Net.NetworkInformation;
using System.Windows.Input;

namespace SimpleFleetManager.ViewModels.Main
{
    public class ForkliftsManagerPageViewModel : BaseViewModel
    {
        #region Variables
        private string? _connectionStatusIconPath;
        public string ConnectionStatusIconPath
        {
            get
            {
                return _connectionStatusIconPath ??= "/Models/Resources/Icons/OfflineRed.png";
            }
            set
            {
                if (_connectionStatusIconPath != value)
                {
                    _connectionStatusIconPath = value;
                    OnPropertyChanged(nameof(ConnectionStatusIconPath));
                }
            }
        }
        private List<Forklift>? _onlineForklifts;
        public List<Forklift> OnlineForklfits
        {
            get
            {
                return _onlineForklifts ??= [];
            }
            set
            {
                if (_onlineForklifts != value)
                {
                    _onlineForklifts = value;
                    OnPropertyChanged(nameof(OnlineForklfits));
                }
            }
        }
        private IEnumerable<Forklift>? _databaseForklifts;
        public IEnumerable<Forklift> DatabaseForklifts
        {
            get
            {
                return _databaseForklifts ??= [];
            }
            set
            {
                if (_databaseForklifts != value)
                {
                    _databaseForklifts = value;
                    OnPropertyChanged(nameof(DatabaseForklifts));
                }
            }
        }
        private Forklift? _currentForklift;
        public Forklift CurrentForklift
        {
            get
            {
                return _currentForklift ??= new();
            }
            set
            {
                if (_currentForklift != value)
                {
                    _currentForklift = value;
                    OnPropertyChanged(nameof(CurrentForklift));
                }
            }
        }
        private readonly ForkliftDataService? _forkliftDataService;
        private readonly ForkliftConnection? _forkliftConnection;
        #endregion
        #region Constructors
        public ForkliftsManagerPageViewModel(ForkliftDataService forkliftDataService, ForkliftConnection forkliftConnection, List<Forklift> onlineForklifts)
        {
            _forkliftDataService = forkliftDataService;
            _forkliftConnection = forkliftConnection;
            _onlineForklifts = onlineForklifts;
            AddForkliftButttonClick = new RelayCommand(ExecuteAddForkliftButtonClick);
            UpdateForkliftButttonClick = new RelayCommand(ExecuteUpdateForkliftButtonClick);
            DeleteForkliftButtonClick = new RelayCommand(ExecuteDeleteForkliftButtonClick);
            SelectForklift = new RelayCommand(ExecuteSelectForklift);
            ConnectButtonClick = new RelayCommand(ExecuteConnectButtonClick);
            PingButtonClick = new RelayCommand(ExecutePingButtonClick);
            GetForklifts();
            ConnectionSstatusIconsSteering();
        }
        #endregion
        #region Page logic
        private async void GetForklifts()
        {
            try
            {
                if (_forkliftDataService != null)
                {
                    DatabaseForklifts = await _forkliftDataService.GetAll();
                }
                else
                {
                    Log.Warning("Forklift data service is null when trying to read all forklfits");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured when tried to get all forklifts from database: " + ex.Message);
            }
        }
        private void ConnectionSstatusIconsSteering()
        {
            try
            {
                _currentForklift ??= new();
                if (_currentForklift.IsConnected)
                {
                    ConnectionStatusIconPath = "/Models/Resources/Icons/OnlineGreen.png";
                }
                else
                {
                    ConnectionStatusIconPath = "/Models/Resources/Icons/OfflineRed.png";
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured when tried to change connection status icon: " + ex.Message);
            }
        }
        #endregion
        #region Logic
        private bool VerifyForkliftData()
        {
            bool result = true;
            _currentForklift ??= new();
            if (string.IsNullOrEmpty(_currentForklift.ForkliftIpAddress))
            {
                result = false;
                Log.Warning("Forklift IP Address is null or empty");
            }
            if (_currentForklift.Port == 0)
            {
                result = false;
                Log.Warning("Forklift Port is 0");
            }
            if (string.IsNullOrEmpty(_currentForklift.Name))
            {
                result = false;
                Log.Warning("Forklift Name is null or empty");
            }
            return result;
        }
        private void PingForklift()
        {
            try
            {
                if (_currentForklift != null && !string.IsNullOrEmpty(_currentForklift.ForkliftIpAddress))
                {
                    Ping ping = new();
                    PingReply reply = ping.Send(_currentForklift.ForkliftIpAddress);
                    if (reply.Status == IPStatus.Success) { Log.Debug("Ping successful: " + reply.RoundtripTime.ToString()); }
                    else { Log.Warning("Ping failed: " + reply.RoundtripTime.ToString()); }
                }
                else
                {
                    if(_currentForklift == null) { Log.Warning("Current selected forklift is NULL"); }
                    if (_currentForklift != null && string.IsNullOrEmpty(_currentForklift.ForkliftIpAddress)) { Log.Warning("Current selected forklift IP Address is NULL or EMPTY"); }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to ping selected forklift: " + ex.Message);
            }
        }
        private async void ConnectToSelectedForklift()
        {
            try
            {
                if (_currentForklift != null && _currentForklift.Client != null && !_currentForklift.Client.Connected && _forkliftConnection != null)
                {
                    if (_currentForklift.IsConnected)
                    {
                        await Task.Run(() => { Task<bool> reconnect = _forkliftConnection.Reconnect(_currentForklift); });
                    }
                    if (!_currentForklift.IsConnected)
                    {
                        await Task.Run(() => { Task<bool> connect = _forkliftConnection.Connect(_currentForklift); });
                    }
                    if (_currentForklift.Client.Connected)
                    {
                        await Task.Run(() => { Task dataExchange =  _forkliftConnection.HandleDataExchange(_currentForklift); });
                        if (_onlineForklifts != null && _onlineForklifts.Count > 0)
                        {
                            if (_onlineForklifts.Count >= _currentForklift.Id && _onlineForklifts.ElementAt(_currentForklift.Id - 1) == null)
                            {
                                _onlineForklifts.Insert(_currentForklift.Id, _currentForklift);
                            }
                            else
                            {
                                _onlineForklifts.Add(_currentForklift);
                            }
                        }
                        else
                        {
                            _onlineForklifts = [];
                            _onlineForklifts.Add(_currentForklift);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured while trying to connect to choosen forklift: " + ex.Message);
            }
        }
        #endregion
        #region Button actions executions
        private async void ExecuteAddForkliftButtonClick(object o)
        {
            try
            {
                if (VerifyForkliftData() && _forkliftDataService != null && _currentForklift != null) 
                {
                    CurrentForklift.BackedUpTebConfig = new();
                    await _forkliftDataService.Create(_currentForklift);
                }
                GetForklifts();
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to add new forklift to database: " + ex.Message);
            }
        }
        private async void ExecuteUpdateForkliftButtonClick(object o)
        {
            try
            {
                GetForklifts();
                if (_databaseForklifts != null && _currentForklift != null && _forkliftDataService != null)
                {
                    foreach (Forklift forklift in _databaseForklifts)
                    {
                        if (forklift.Id == _currentForklift.Id)
                        {
                            if (VerifyForkliftData())
                            {
                                await _forkliftDataService.Update(_currentForklift.Id, _currentForklift);
                            }
                            else
                            {
                                Log.Warning("Updated forklift data verification failed!");
                            }
                        }
                    }
                }
                else
                {
                    if (_databaseForklifts == null) { Log.Warning("List of forklifts readed from database is null"); }
                    if (_currentForklift == null) { Log.Warning("Current selected forklift is empty!"); }
                    if (_forkliftDataService == null) { Log.Warning("Forklift data service is NULL"); }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to update forklift in database: " + ex.Message);
            }
        }
        private async void ExecuteDeleteForkliftButtonClick(object o)
        {
            try
            {
                if (_currentForklift != null && _forkliftDataService != null)
                {
                    GetForklifts();
                    if (_databaseForklifts != null)
                    {
                        foreach (Forklift forklift in _databaseForklifts)
                        {
                            if (forklift.Id == _currentForklift.Id)
                            {
                                if (await _forkliftDataService.Delete(_currentForklift.Id))
                                {
                                    Log.Debug("Forklift delete SUCCESS");
                                }
                                else
                                {
                                    Log.Debug("Forklift delete failed");
                                }
                            }
                        }
                    }
                    else
                    {
                        Log.Warning("List of forklifts readed from database is null");
                    }
                }
                else
                {
                    if (_forkliftDataService == null) { Log.Warning("Forklift data service is NULL"); }
                    if (_currentForklift == null) { Log.Warning("Current selected forklift is empty!"); }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to delete existing forklift from database: " + ex.Message);
            }
        }
        private async void ExecuteSelectForklift(object o)
        {
            try
            {
                if (Convert.ToInt32(o) > 0 && _forkliftDataService != null)
                {
                    CurrentForklift = await _forkliftDataService.Get(Convert.ToInt32(o));
                    if (_currentForklift != null)
                    {
                        Log.Debug("Selected forklift: " + _currentForklift.Name);
                        if (_onlineForklifts != null)
                        {
                            foreach (Forklift forklift in _onlineForklifts)
                            {
                                if (forklift.Id == _currentForklift.Id)
                                {
                                    CurrentForklift.IsConnected = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        Log.Warning("Current forklift (private) is still null");
                    }
                }
                else
                {
                    if (_forkliftDataService == null) { Log.Warning("Forklift data service is NULL"); }
                    if (Convert.ToInt32(o) <= 0) { Log.Error("Wrong object value given by button action: " +  Convert.ToString(o)); }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to select forklift from list: " + ex.Message);
            }
        }
        private void ExecuteConnectButtonClick(object o)
        {
            if (_currentForklift != null)
            {
                ConnectToSelectedForklift();
                ConnectionSstatusIconsSteering();
            }
        }
        private void ExecutePingButtonClick(object o)
        {
            try
            {
                PingForklift();
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to execute ping button click: " + ex.Message);
            }
        }
        #endregion
        #region Button's Icommands declarations
        public ICommand? AddForkliftButttonClick { get; private set; }
        public ICommand? UpdateForkliftButttonClick { get; private set; }
        public ICommand? DeleteForkliftButtonClick { get; private set; }
        public ICommand? SelectForklift {  get; private set; }
        public ICommand? ConnectButtonClick { get; private set; }
        public ICommand? PingButtonClick { get; private set; }
        #endregion
    }
}
