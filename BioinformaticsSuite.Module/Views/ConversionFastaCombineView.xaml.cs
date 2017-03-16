using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ConversionFastaCombineView.xaml
    /// </summary>
    public partial class ConversionFastaCombineView : UserControl
    {
        public ConversionFastaCombineView(ConversionFastaCombineViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}