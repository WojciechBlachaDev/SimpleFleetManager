using SimpleFleetManager.ViewModels.Main;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.Main
{
    /// <summary>
    /// Logika interakcji dla klasy ForkliftsPage.xaml
    /// </summary>
    public partial class ForkliftsPage : Page
    {
        public ForkliftsPage(ForkliftsPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
