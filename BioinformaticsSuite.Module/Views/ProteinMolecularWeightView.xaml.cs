using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ProteinMolecularWeightView.xaml
    /// </summary>
    public partial class ProteinMolecularWeightView : UserControl
    {
        public ProteinMolecularWeightView(ProteinMolecularWeightViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}