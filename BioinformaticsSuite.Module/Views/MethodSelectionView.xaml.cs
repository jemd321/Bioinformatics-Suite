﻿using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for MethodSelectionView.xaml
    /// </summary>
    public partial class MethodSelectionView
    {
        public MethodSelectionView(MethodSelectionViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}