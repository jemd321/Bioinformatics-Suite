using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ConversionEMBLFasta.xaml
    /// </summary>
    public partial class ConversionEmblFastaView
    {
        public ConversionEmblFastaView(ConversionEmblFastaViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}