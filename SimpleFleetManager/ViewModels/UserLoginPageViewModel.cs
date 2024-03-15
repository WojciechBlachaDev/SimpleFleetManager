using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleFleetManager.ViewModels
{
    public class UserLoginPageViewModel : BaseViewModel
    {
        #region Variables
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
        public UserLoginPageViewModel(UserStore userStore, UserDataService userDataService)
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

        }
        private void ExecuteLogoutButtonClick(object o)
        {

        }
        private void ExecuteSelectUserFromList(object o)
        {

        }
        #endregion
        #region Buttons Icommand declarations
        public ICommand LoginButtonClick { get; private set; }
        public ICommand LogoutButtonClick { get; private set; }
        public ICommand SelectUserFromList {  get; private set; }
        #endregion
    }
}
