using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ConversionGenbankFasta.xaml
    /// </summary>
    public partial class ConversionGenbankFastaView : UserControl
    {
        public ConversionGenbankFastaView(ConversionGenbankFastaViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}