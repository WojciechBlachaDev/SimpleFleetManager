using SimpleFleetManager.ViewModels.ForkliftPages;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.ForkliftPages
{
    /// <summary>
    /// Logika interakcji dla klasy ErrorsPage.xaml
    /// </summary>
    public partial class ErrorsPage : Page
    {
        public ErrorsPage(ErrorsPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
