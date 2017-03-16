using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for RnaMolecularWeightView.xaml
    /// </summary>
    public partial class RnaMolecularWeightView
    {
        public RnaMolecularWeightView(RnaMolecularWeightViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}