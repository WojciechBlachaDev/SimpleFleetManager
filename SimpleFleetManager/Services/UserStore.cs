using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Interfaces;
namespace SimpleFleetManager.Services
{
    public class UserStore : IUserStore
    {
        private User? _currentUser;
        public User CurrentUser
        {
            get { return _currentUser ??= new(); }
            set { if (_currentUser != value) { _currentUser = value; StateChanged?.Invoke(); } }
        }
            public event Action? StateChanged;
    }
}

