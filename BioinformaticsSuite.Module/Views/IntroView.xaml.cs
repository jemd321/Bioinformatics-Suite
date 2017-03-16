using System.Windows.Controls;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.Module.Views
{
    /// <summary>
    ///     Interaction logic for IntroView.xaml
    /// </summary>
    public partial class IntroView : UserControl
    {
        public IntroView(IntroViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}