using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaReadingFrameView.xaml
    /// </summary>
    public partial class DnaReadingFrameView : UserControl
    {
        public DnaReadingFrameView(DnaReadingFrameViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}