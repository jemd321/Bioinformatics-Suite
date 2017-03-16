using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ConversionGenbankFasta.xaml
    /// </summary>
    public partial class ConversionGenbankFastaView
    {
        public ConversionGenbankFastaView(ConversionGenbankFastaViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}