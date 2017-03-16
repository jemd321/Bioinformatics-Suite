using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for RnaMolecularWeightView.xaml
    /// </summary>
    public partial class RnaMolecularWeightView : UserControl
    {
        public RnaMolecularWeightView(RnaMolecularWeightViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}