using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ConversionGenbankTranslate.xaml
    /// </summary>
    public partial class ConversionGenbankTranslateView : UserControl
    {
        public ConversionGenbankTranslateView(ConversionGenbankTranslateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}