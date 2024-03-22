using SimpleFleetManager.ViewModels.ForkliftPages;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.ForkliftPages
{
    /// <summary>
    /// Logika interakcji dla klasy LogPage.xaml
    /// </summary>
    public partial class LogPage : Page
    {
        public LogPage(LogsPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
