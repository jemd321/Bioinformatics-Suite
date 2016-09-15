using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BioinformaticsSuite.Module.ViewModels;
using Prism.Mvvm;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    /// Interaction logic for MethodSelectionView.xaml
    /// </summary>
    public partial class MethodSelectionView : UserControl
    {
        public MethodSelectionView(MethodSelectionViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
