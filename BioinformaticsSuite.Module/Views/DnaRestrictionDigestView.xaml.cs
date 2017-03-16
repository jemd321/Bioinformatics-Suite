using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaRestrictionDigestView.xaml
    /// </summary>
    public partial class DnaRestrictionDigestView
    {
        public DnaRestrictionDigestView(DnaRestricitionDigestViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}