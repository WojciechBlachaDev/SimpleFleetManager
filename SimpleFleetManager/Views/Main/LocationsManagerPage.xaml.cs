using SimpleFleetManager.ViewModels.Main;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.Main
{
    /// <summary>
    /// Logika interakcji dla klasy LocationsManagerPage.xaml
    /// </summary>
    public partial class LocationsManagerPage : Page
    {
        public LocationsManagerPage(LocationsManagerPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
