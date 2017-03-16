using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaMolecularWeightView.xaml
    /// </summary>
    public partial class DnaMolecularWeightView
    {
        public DnaMolecularWeightView(DnaMolecularWeightViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}