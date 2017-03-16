using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for DnaStatisticsView.xaml
    /// </summary>
    public partial class DnaStatisticsView : UserControl
    {
        public DnaStatisticsView(DnaStatisticsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}