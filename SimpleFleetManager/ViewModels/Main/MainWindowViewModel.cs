using Serilog;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services;
using SimpleFleetManager.Services.Communication;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
using SimpleFleetManager.Views.Main;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleFleetManager.ViewModels.Main
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Variables
        private readonly UserDataService _userDataService;
        private readonly LocationDataService _locationDataService;
        private readonly ForkliftDataService _forkliftDataService;
        private readonly ForkliftConnection _forkliftConnection;
        private readonly UserStore _userStore;
        private readonly JobStepDataService _jobStepDataService;
        private readonly JobDataService _jobdataService;
        private readonly LogsDataService _logsDataService;
        private IEnumerable<Forklift>? _readedForklifts;
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
        private bool _showShutdownDialog;
        public bool ShowShutdownDialog
        {
            get
            {
                return _showShutdownDialog;
            }
            set
            {
                _showShutdownDialog = value;
                OnPropertyChanged(nameof(ShowShutdownDialog));
            }
        }
        private User? _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser ??= new();
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        private string? _loginPageIcon;
        public string LoginPageIcon
        {
            get
            {
                return _loginPageIcon ??= "Models/Resources/Icons/LoginPageWhite.png";
            }
            set
            {
                _loginPageIcon = value;
                OnPropertyChanged(nameof(LoginPageIcon));
            }
        }
        private bool _clientMenuVisible;
        public bool ClientMenuVisible
        {
            get
            {
                return _clientMenuVisible;
            }
            set
            {
                _clientMenuVisible = value;
                OnPropertyChanged(nameof(ClientMenuVisible));
            }
        }
        private bool _installatorMenuVisible;
        public bool InstallatorMenuVisible
        {
            get
            {
                return _installatorMenuVisible;
            }
            set
            {
                _installatorMenuVisible = value;
                OnPropertyChanged(nameof(InstallatorMenuVisible));
            }
        }
        private bool _adminMenuVisible;
        public bool AdminMenuVisible
        {
            get
            {
                return _adminMenuVisible;
            }
            set
            {
                _adminMenuVisible = value;
                OnPropertyChanged(nameof(AdminMenuVisible));
            }
        }
        private List<Forklift>? _connectedForklifts;
        public List<Forklift> ConnectedForklifts
        {
            get
            {
                return _connectedForklifts ??= [];
            }
            set
            {
                _connectedForklifts = value;
                OnPropertyChanged(nameof(ConnectedForklifts));
            }
        }
        #endregion
        #endregion
        #region Constructors
        public MainWindowViewModel(UserDataService userDataService,
            JobStepDataService jobStepDataService,
            JobDataService jobDataService,
            ForkliftDataService forkliftDataService,
            LocationDataService locationDataService,
            UserStore userStore,
            ForkliftConnection forkliftConnection,
            LogsDataService logsDataService)
        {
            #region Services
            _userDataService = userDataService;
            _jobStepDataService = jobStepDataService;
            _jobdataService = jobDataService;
            _forkliftDataService = forkliftDataService;
            _locationDataService = locationDataService;
            _userStore = userStore;
            _forkliftConnection = forkliftConnection;
            _logsDataService = logsDataService;
            #endregion
            #region Buttton actions
            ShutdownAppButtonClick = new RelayCommand(ExecuteShutdownAppButtonClick);
            LoginPageButtonClick = new RelayCommand(ExecuteLoginPageButtonClick);
            ForkliftsPageButtonClick = new RelayCommand(ExecuteForkliftsPageButtonClick);
            ForkliftManagerPageButtonClick = new RelayCommand(ExecuteForkliftManagerPageButtonclick);
            UsersManagerPageButtonClick = new RelayCommand(ExecuteUsersManagerPageButtonClick);
            LocationsManagerPageButtonClick = new RelayCommand(ExecuteLocationsManagerPageButtonClick);
            ManualTaskCreatorPageButtonClick = new RelayCommand(ExecuteManualTaskCreatorPageButtonClick);
            #endregion
            #region Page start setup
            ShowMenu = true;
            _userStore.StateChanged += OnUserStateChanged;
            ConnectToForklifts();
            #endregion
        }
        #endregion
        #region Page logic
        private void OnUserStateChanged()
        {
            try
            {
                if (_userStore.CurrentUser != null)
                {
                    CurrentUser = _userStore.CurrentUser;
                    LoginPageIcon = "/Models/Resources/Icons/LogoutPageWhite.png";
                }
                else
                {
                    LoginPageIcon = "/Models/Resources/Icons/LoginPageWhite.png";
                }
                SetMenuVisibility();
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when selecting login page icon: " + ex.Message);
            }
        }
        private void SetMenuVisibility()
        {
            try
            {
                if (_userStore.CurrentUser.AccessLevel == 1)
                {
                    ClientMenuVisible = true;
                    InstallatorMenuVisible = true;
                    AdminMenuVisible = true;
                }
                else if (_userStore.CurrentUser.AccessLevel == 2)
                {
                    ClientMenuVisible = true;
                    InstallatorMenuVisible = true;
                    AdminMenuVisible = false;
                }
                else if (_userStore.CurrentUser.AccessLevel == 3)
                {
                    ClientMenuVisible = true;
                    InstallatorMenuVisible = false;
                    AdminMenuVisible = false;
                }
                else
                {
                    ClientMenuVisible = false;
                    InstallatorMenuVisible = false;
                    AdminMenuVisible = false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when setting main menu visibility by access level: " + ex.Message);
            }
        }
        #endregion
        #region Main logic
        private async void ConnectToForklifts()
        {
            try
            {
                _readedForklifts = await _forkliftDataService.GetAll();
                foreach (Forklift forklift in _readedForklifts)
                {
                    forklift.Client ??= new();
                    if (!forklift.IsConnected)
                    {
                        await Task.Run(() => { Task<bool> connect = _forkliftConnection.Connect(forklift); });
                        await Task.Delay(50);
                    }
                    if (forklift.Client.Connected)
                    {
                        await Task.Run(() => { Task dataEchange = _forkliftConnection.HandleDataExchange(forklift, _logsDataService); });
                        if (_connectedForklifts != null && _connectedForklifts.Count > 0)
                        {
                            if (forklift.Id >= _connectedForklifts.Count && _connectedForklifts.ElementAt(forklift.Id - 1) == null)
                            {
                                _connectedForklifts.Insert(forklift.Id, forklift);
                            }
                            else
                            {
                                _connectedForklifts.Add(forklift);
                            }
                        }
                        else
                        {
                            _connectedForklifts = [];
                            ConnectedForklifts.Add(forklift);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to connect to forklifts saved in database: " + ex.Message);
            }
        }
        #endregion
        #region Buttons actions execution
        private void ExecuteShutdownAppButtonClick(object? o)
        {
            Application.Current.Shutdown();
        }
        private void ExecuteLoginPageButtonClick(object? o)
        {
            Log.Debug("Login page button clicked");
            CurrentPage = new LoginPage(new LoginPageViewModel(_userStore, _userDataService));
            ShowMenu = false;
        }
        private void ExecuteForkliftsPageButtonClick(object? o)
        {
            Log.Debug("Forklifts page button clicked");
            _connectedForklifts ??= [];
            CurrentPage = new ForkliftsPage(new ForkliftsPageViewModel(_userStore, _forkliftDataService, _connectedForklifts, _logsDataService));
            ShowMenu = false;
        }
        private void ExecuteUsersManagerPageButtonClick(object? o)
        {
            Log.Debug("Users Manager Page buttton clicked");
            CurrentPage = new UsersManagerPage(new UsersManagerPageViewModel(_userStore, _userDataService));
            ShowMenu = false;
        }
        private void ExecuteForkliftManagerPageButtonclick(object? o)
        {
            Log.Debug("Forklift Manager page button clicked");
            _connectedForklifts ??= [];
            CurrentPage = new ForkliftManagementPage(new ForkliftsManagerPageViewModel(_forkliftDataService, _forkliftConnection, _connectedForklifts));
            ShowMenu = false;
        }
        private void ExecuteLocationsManagerPageButtonClick(object? o)
        {
            Log.Debug("Locations Manager page button clicked");
            _connectedForklifts ??= [];
            CurrentPage = new LocationsManagerPage(new LocationsManagerPageViewModel(_locationDataService, _connectedForklifts));
            ShowMenu = false;
        }
        private void ExecuteManualTaskCreatorPageButtonClick(object? o)
        {
            Log.Debug("Manual task creator page buttton clicked");
            CurrentPage = new ManualTaskCreatorPage(new ManualTasksCreatorPageViewModel(_jobStepDataService, _jobdataService, _locationDataService));
            ShowMenu = false;
        }
        #endregion
        #region Button's Icommands declarations
        public ICommand? ShutdownAppButtonClick { get; private set; }
        public ICommand? LoginPageButtonClick { get; private set; }
        public ICommand? ForkliftsPageButtonClick { get; private set; }
        public ICommand? UsersManagerPageButtonClick { get; private set; }
        public ICommand? ForkliftManagerPageButtonClick { get; private set; }
        public ICommand? LocationsManagerPageButtonClick { get; private set; }
        public ICommand? ManualTaskCreatorPageButtonClick { get; private set; }
        #endregion
    }
}
