using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ProteinOpenReadingFrameView.xaml
    /// </summary>
    public partial class ProteinOpenReadingFrameView
    {
        public ProteinOpenReadingFrameView(ProteinOpenReadingFrameViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}