using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ConversionEMBLTranslate.xaml
    /// </summary>
    public partial class ConversionEmblTranslateView
    {
        public ConversionEmblTranslateView(ConversionEmblTranslateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}