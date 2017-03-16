using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaStatisticsView.xaml
    /// </summary>
    public partial class DnaStatisticsView
    {
        public DnaStatisticsView(DnaStatisticsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}