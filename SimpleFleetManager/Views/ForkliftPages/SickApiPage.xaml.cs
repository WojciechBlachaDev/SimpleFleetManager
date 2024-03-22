using SimpleFleetManager.ViewModels.ForkliftPages;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.ForkliftPages
{
    /// <summary>
    /// Logika interakcji dla klasy SickApiPage.xaml
    /// </summary>
    public partial class SickApiPage : Page
    {
        public SickApiPage(SickApiPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
