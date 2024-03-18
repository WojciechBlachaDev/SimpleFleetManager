using SimpleFleetManager.ViewModels.Main;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.Main
{
    /// <summary>
    /// Logika interakcji dla klasy LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(LoginPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
