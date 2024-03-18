using Serilog;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
using System.Windows.Input;

namespace SimpleFleetManager.ViewModels.Main
{
    public class UsersManagerPageViewModel : BaseViewModel
    {
        #region Variables
        private readonly UserStore _userStore;
        private readonly UserDataService _userDataService;
        private User? _selectedUser;
        public User SelectedUser
        {
            get
            {
                return _selectedUser = new();
            }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        private string? _checkPassword;
        public string CheckPassword
        {
            get
            {
                return _checkPassword ?? string.Empty;
            }
            set
            {
                _checkPassword = value;
                OnPropertyChanged(nameof(CheckPassword));
            }
        }
        private IEnumerable<User>? _users;
        public IEnumerable<User> Users
        {
            get
            {
                return _users ?? [];
            }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        private bool _isClient;
        public bool IsClient
        {
            get
            {
                return _isClient;
            }
            set
            {
                _isClient = value;
                OnPropertyChanged(nameof(IsClient));
            }
        }
        private bool _isInstallator;
        public bool IsInstallator
        {
            get
            {
                return _isInstallator;
            }
            set
            {
                _isInstallator = value;
                OnPropertyChanged(nameof(IsInstallator));
            }
        }
        private bool _isAdmin;
        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }
        #endregion
        #region Constructors
        public UsersManagerPageViewModel(UserStore userStore, UserDataService userDataService)
        {
            _userStore = userStore;
            _userDataService = userDataService;
            SelectedUser ??= new();
            SelectUserFromList = new RelayCommand(ExecuteSelectUserFromList);
            DeleteUserButtonClick = new RelayCommand(ExecuteDeleteUserButtonClick);
            UpdateUserButtonClick = new RelayCommand(ExecuteUpdateUserButtonClick);
            AddNewUserButtonClick = new RelayCommand(ExecuteAddNewUserButtonClick);
            RefreshUsersList();
        }
        #endregion
        #region Logic
        private async void RefreshUsersList()
        {
            try
            {
                if (_userDataService != null)
                {
                    Users = await _userDataService.GetAll();
                }
                else
                {
                    Log.Warning("User data service is NULL when refreshing existing users list");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to refresh existing users list: " + ex.Message);
            }

        }
        private bool CheckAccessLevel(User user)
        {
            bool result = true;
            if (_userStore != null && user != null && _userDataService != null)
            {
                if (_userStore.CurrentUser.AccessLevel == 3)
                {
                    if (user.AccessLevel == 1 || user.AccessLevel == 2)
                    {
                        result = false;
                    }
                }
                if (_userStore.CurrentUser.AccessLevel == 2)
                {
                    if (user.AccessLevel == 3)
                    {
                        result = false;
                    }
                }
                /*if (_userStore.CurrentUser.AccessLevel < 1 || _userStore.CurrentUser.AccessLevel > 3)
                {
                    result = false;
                }*/
            }
            return result;
        }
        private bool VerifyUserData()
        {
            bool result = true;
            if (_selectedUser != null && _userDataService != null)
            {
                if (_selectedUser.Username == null)
                {
                    result = false;
                    Log.Warning("Username is empty!");
                }
                if (_selectedUser.Password == null)
                {
                    result = false;
                    Log.Warning("Password is empty!");
                }
                if (string.IsNullOrEmpty(_checkPassword))
                {
                    result = false;
                    Log.Warning("Check password box is null or empty");
                }
                if (_selectedUser.Password != _checkPassword)
                {
                    result = false;
                    Log.Warning("Check password box is not the same as Password box!");
                }
            }
            return result;
        }
        #endregion
        #region Buttons execution
        private async void ExecuteAddNewUserButtonClick(object o)
        {
            try
            {
                User newUser = new();
                if (VerifyUserData() && _selectedUser != null && newUser != null && _userDataService != null)
                {
                    newUser = _selectedUser;
                    if (_isClient)
                    {
                        newUser.AccessLevel = 3;
                    }
                    else if (_isInstallator)
                    {
                        newUser.AccessLevel = 2;
                    }
                    else if (_isAdmin)
                    {
                        newUser.AccessLevel = 1;
                    }
                    else
                    {
                        newUser.AccessLevel = 3;
                        Log.Warning("Wrong user access level !");
                    }
                    if (CheckAccessLevel(newUser))
                    {
                        await _userDataService.Create(newUser);
                    }
                    else
                    {
                        Log.Warning("Access level to low for creating this user");
                    }
                }
                else
                {
                    if (!VerifyUserData())
                    {
                        Log.Warning("User data verification failed when creating new user");
                    }
                    if (_selectedUser == null)
                    {
                        Log.Warning("Selected user is null when creating new user");
                    }
                    if (newUser == null)
                    {
                        Log.Warning("New user object is null when creating new user");
                    }
                    if (_userDataService == null)
                    {
                        Log.Warning("User data service (database) is null when creating new user");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception catched while creating new user: " + ex.Message);
            }
        }
        private async void ExecuteUpdateUserButtonClick(object o)
        {
            try
            {
                bool userCheck = VerifyUserData();
                if (userCheck && _selectedUser != null && _userDataService != null)
                {
                    bool accessLevelOk = CheckAccessLevel(_selectedUser);
                    if (accessLevelOk)
                    {
                        if (_isClient)
                        {
                            _selectedUser.AccessLevel = 3;
                        }
                        else if (_isInstallator)
                        {
                            _selectedUser.AccessLevel = 2;
                        }
                        else if (_isAdmin)
                        {
                            _selectedUser.AccessLevel = 1;
                        }
                        else
                        {
                            _selectedUser.AccessLevel = 3;
                            Log.Warning("Wrong user access level !");
                        }
                        await _userDataService.Update(_selectedUser.Id, _selectedUser);
                        RefreshUsersList();
                    }
                    else
                    {
                        Log.Warning("Current user privileges to low while updating user data");
                    }
                }
                if (!userCheck)
                {
                    Log.Warning("Wrong user data while updating user data");
                }
                if (_selectedUser == null)
                {
                    Log.Warning("Selected user is NULL while updating user data");
                }
                if (_userDataService == null)
                {
                    Log.Warning("User data service is NULL while updating user data");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception catched while editin user data: " + ex.Message);
            }

        }
        private async void ExecuteDeleteUserButtonClick(object o)
        {
            try
            {
                if (_selectedUser != null && _userDataService != null)
                {
                    if (CheckAccessLevel(_selectedUser))
                    {
                        await _userDataService.Delete(_selectedUser.Id);
                    }
                    else
                    {
                        Log.Warning("Access level to low while trying to delete user");
                    }
                }
                if (_selectedUser == null)
                {
                    Log.Warning("Selected user is NULL while trying to delete user");
                }
                if (_userDataService == null)
                {
                    Log.Warning("User data service is NULL while trying to delete user");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured while deleting existing user: " + ex.Message);
            }
        }
        private async void ExecuteSelectUserFromList(object o)
        {
            try
            {
                if (o != null && _userDataService != null)
                {
                    SelectedUser = await _userDataService.Get(Convert.ToInt32(o));
                }
                if (o == null)
                {
                    Log.Warning("selected user is NULL when selecting user from list");
                }
                if (_userDataService == null)
                {
                    Log.Warning("User data service is null when selecting user from list");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occured while selecting user from list: " + ex.Message);
            }
        }
        #endregion
        #region ICommand declarations
        public ICommand? AddNewUserButtonClick { get; private set; }
        public ICommand? UpdateUserButtonClick { get; private set; }
        public ICommand? DeleteUserButtonClick { get; private set; }
        public ICommand? SelectUserFromList { get; private set; }
        #endregion
    }
}
