using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaTranscribeView.xaml
    /// </summary>
    public partial class DnaTranscribeView
    {
        public DnaTranscribeView(DnaTranscribeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}