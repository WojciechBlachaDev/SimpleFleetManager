using SimpleFleetManager.ViewModels.Main;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.Main
{
    /// <summary>
    /// Logika interakcji dla klasy ForkliftManagementPage.xaml
    /// </summary>
    public partial class ForkliftManagementPage : Page
    {
        public ForkliftManagementPage(ForkliftsManagerPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
