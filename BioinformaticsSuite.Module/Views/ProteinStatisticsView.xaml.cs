using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for ProteinStatisticsView.xaml
    /// </summary>
    public partial class ProteinStatisticsView : UserControl
    {
        public ProteinStatisticsView(ProteinStatisticsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}