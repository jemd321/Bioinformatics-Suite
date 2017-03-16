using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaRestrictionDigestView.xaml
    /// </summary>
    public partial class DnaRestrictionDigestView : UserControl
    {
        public DnaRestrictionDigestView(DnaRestricitionDigestViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}