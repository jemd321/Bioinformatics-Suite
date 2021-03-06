﻿using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaTranslateView.xaml
    /// </summary>
    public partial class DnaTranslateView
    {
        public DnaTranslateView(DnaTranslateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}