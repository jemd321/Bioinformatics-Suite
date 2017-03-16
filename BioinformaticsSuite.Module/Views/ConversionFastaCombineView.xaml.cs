using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ConversionFastaCombineView.xaml
    /// </summary>
    public partial class ConversionFastaCombineView
    {
        public ConversionFastaCombineView(ConversionFastaCombineViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}