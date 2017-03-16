using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ConversionFastSplitView.xaml
    /// </summary>
    public partial class ConversionFastaSplitView
    {
        public ConversionFastaSplitView(ConversionFastaSplitViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}