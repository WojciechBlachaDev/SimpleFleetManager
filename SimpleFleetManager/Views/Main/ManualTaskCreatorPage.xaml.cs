using SimpleFleetManager.ViewModels.Main;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.Main
{
    /// <summary>
    /// Logika interakcji dla klasy ManualTaskCreatorPage.xaml
    /// </summary>
    public partial class ManualTaskCreatorPage : Page
    {
        public ManualTaskCreatorPage(ManualTasksCreatorPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
