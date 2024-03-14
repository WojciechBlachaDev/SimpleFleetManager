using MahApps.Metro.Controls;
using SimpleFleetManager.ViewModels;
using System.Windows;

namespace SimpleFleetManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}