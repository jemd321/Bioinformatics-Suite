using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ProteinStatisticsView.xaml
    /// </summary>
    public partial class ProteinStatisticsView
    {
        public ProteinStatisticsView(ProteinStatisticsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}