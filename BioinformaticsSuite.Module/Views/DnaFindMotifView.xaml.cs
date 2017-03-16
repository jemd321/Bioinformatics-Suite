using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaFindMotifView.xaml
    /// </summary>
    public partial class DnaFindMotifView
    {
        public DnaFindMotifView(DnaFindMotifViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}