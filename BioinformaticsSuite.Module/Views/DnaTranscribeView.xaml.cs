using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaTranscribeView.xaml
    /// </summary>
    public partial class DnaTranscribeView : UserControl
    {
        public DnaTranscribeView(DnaTranscribeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}