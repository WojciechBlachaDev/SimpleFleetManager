﻿using SimpleFleetManager.ViewModels.ForkliftPages;
using System.Windows.Controls;

namespace SimpleFleetManager.Views.ForkliftPages
{
    /// <summary>
    /// Logika interakcji dla klasy ActualParametersPage.xaml
    /// </summary>
    public partial class ActualParametersPage : Page
    {
        public ActualParametersPage(ActualParametersPageVIewModel vIewModel)
        {
            InitializeComponent();
            DataContext = vIewModel;
        }
    }
}
