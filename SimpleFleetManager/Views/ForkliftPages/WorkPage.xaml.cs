using SimpleFleetManager.ViewModels.ForkliftPages;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.ForkliftPages
{
    /// <summary>
    /// Logika interakcji dla klasy WorkPage.xaml
    /// </summary>
    public partial class WorkPage : Page
    {
        public WorkPage(WorkPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
