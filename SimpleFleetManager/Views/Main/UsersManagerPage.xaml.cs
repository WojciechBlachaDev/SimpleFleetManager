using SimpleFleetManager.ViewModels.Main;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.Main
{
    /// <summary>
    /// Logika interakcji dla klasy UsersManagerPage.xaml
    /// </summary>
    public partial class UsersManagerPage : Page
    {
        public UsersManagerPage(UsersManagerPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
