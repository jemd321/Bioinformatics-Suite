using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Interactivity.InteractionRequest;

namespace BioinformaticsSuite.Module.Views.PopupViews
{
    /// <summary>
    /// Interaction logic for DnaHelpPopupView.xaml
    /// </summary>
    public partial class DnaHelpPopupView : UserControl, IInteractionRequestAware
    {
        public DnaHelpPopupView()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.FinishInteraction != null)
            {
                this.FinishInteraction();
            }
        }

        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }
    }
}
