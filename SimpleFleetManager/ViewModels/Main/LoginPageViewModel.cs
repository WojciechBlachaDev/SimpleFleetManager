using Serilog;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
using System.Windows.Input;

namespace SimpleFleetManager.ViewModels.Main
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Variables
        private string? _username;
        public string Username
        {
            get
            {
                return _username ?? string.Empty;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string? _password;
        public string Password
        {
            get
            {
                return _password ?? string.Empty;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private User? _selectedUser;
        public User SelectedUser
        {
            get
            {
                return _selectedUser ?? new();
            }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        private bool _isLoginButtonVisible;
        public bool IsLoginButtonVisible
        {
            get
            {
                return _isLoginButtonVisible;
            }
            set
            {
                _isLoginButtonVisible = value;
                OnPropertyChanged(nameof(IsLoginButtonVisible));
            }
        }
        private bool _isLogoutButtonVisible;
        public bool IsLogoutButtonVisible
        {
            get
            {
                return _isLogoutButtonVisible;
            }
            set
            {
                _isLogoutButtonVisible = value;
                OnPropertyChanged(nameof(IsLogoutButtonVisible));
            }
        }
        private IEnumerable<User>? _avaibleUsers;
        public IEnumerable<User> AvaibleUsers
        {
            get
            {
                return _avaibleUsers ?? [];
            }
            set
            {
                _avaibleUsers = value;
                OnPropertyChanged(nameof(AvaibleUsers));
            }
        }
        private readonly UserStore _userStore;
        private readonly UserDataService _userDataService;
        #endregion
        #region Constructors
        public LoginPageViewModel(UserStore userStore, UserDataService userDataService)
        {
            _userStore = userStore;
            _userDataService = userDataService;
            GetSavedUsers();
            ButtonSelector();
            LoginButtonClick = new RelayCommand(ExecuteLoginButtonClick);
            LogoutButtonClick = new RelayCommand(ExecuteLogoutButtonClick);
            SelectUserFromList = new RelayCommand(ExecuteSelectUserFromList);
        }
        #endregion
        #region Logic
        private async void GetSavedUsers()
        {
            AvaibleUsers = await _userDataService.GetAll();
        }
        private bool CheckUser()
        {
            _avaibleUsers ??= [];
            foreach (User user in _avaibleUsers)
            {
                if (user.Username == _username && user.Password == _password)
                {
                    _userStore.CurrentUser = user;
                    Log.Debug("Check user result: SUCCESS");
                    return true;
                }
            }
            Log.Information("Incorrect user name or password");
            return false;
        }
        public void ButtonSelector()
        {
            if (_userStore.CurrentUser != null && _userStore.CurrentUser.IsLogged)
            {
                IsLoginButtonVisible = false;
                IsLogoutButtonVisible = true;
            }
            else
            {
                IsLoginButtonVisible = true;
                IsLogoutButtonVisible = false;
            }
        }
        #endregion
        #region Buttons logic
        private void ExecuteLoginButtonClick(object o)
        {
            try
            {
                if (!string.IsNullOrEmpty(_password) && !string.IsNullOrEmpty(_username) && o != null)
                {
                    if (CheckUser())
                    {
                        _userStore.CurrentUser.IsLogged = true;
                        ButtonSelector();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error when user attempted to login: " + ex.Message);
            }
        }
        private void ExecuteLogoutButtonClick(object o)
        {
            try
            {
                if (_userStore.CurrentUser != null && o != null)
                {
                    _userStore.CurrentUser = new();
                    ButtonSelector();
                    Log.Debug("Logut action: SUCCESS");
                }
                else
                {
                    if (_userStore.CurrentUser == null)
                    {
                        Log.Warning("User store current user is null when attempting to logout");
                    }
                    if (o == null)
                    {
                        Log.Warning("Button parameter is null when attempting to logout");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when user attempted to logout: " + ex.Message);
            }
        }
        private void ExecuteSelectUserFromList(object o)
        {

        }
        #endregion
        #region Buttons Icommand declarations
        public ICommand LoginButtonClick { get; private set; }
        public ICommand LogoutButtonClick { get; private set; }
        public ICommand SelectUserFromList { get; private set; }
        #endregion
    }
}
