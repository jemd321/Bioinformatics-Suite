using System;
using System.Windows;
using Prism.Interactivity.InteractionRequest;

namespace BioinformaticsSuite.Module.Views.PopupViews
{
    /// <summary>
    ///     Interaction logic for DnaHelpPopupView.xaml
    /// </summary>
    public partial class DnaHelpPopupView : IInteractionRequestAware
    {
        public DnaHelpPopupView()
        {
            InitializeComponent();
        }

        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            FinishInteraction?.Invoke();
        }
    }
}