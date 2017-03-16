using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for RnaTranslateView.xaml
    /// </summary>
    public partial class RnaTranslateView : UserControl
    {
        public RnaTranslateView(RnaTranslateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}