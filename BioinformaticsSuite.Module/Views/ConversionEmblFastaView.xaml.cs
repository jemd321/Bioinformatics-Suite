using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ConversionEMBLFasta.xaml
    /// </summary>
    public partial class ConversionEmblFastaView : UserControl
    {
        public ConversionEmblFastaView(ConversionEmblFastaViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}