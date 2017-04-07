using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BioinformaticsSuite.Module.Popups;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace BioinformaticsSuite.Module.ViewModels.PopupViewModels
{
    public class SequenceValidatationPopupViewModel : BindableBase, IInteractionRequestAware
    {
        private SequenceValidationNotification _notification;

        public SequenceValidatationPopupViewModel()
        {
            SelectItemCommand = new DelegateCommand(AcceptSelectedItem);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }

        public Action FinishInteraction { get; set; }

        public INotification Notification
        {
            get
            {
                return _notification;
            }
            set
            {
                if (!(value is SequenceValidationNotification)) return;
                _notification = (SequenceValidationNotification) value;
                OnPropertyChanged(() => Notification);
            }
        }

        public string SelectedItem { get; set; }

        public ICommand SelectItemCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public void AcceptSelectedItem()
        {
            if (_notification != null)
            {
                
                _notification.Confirmed = true;
            }

            FinishInteraction();
        }

        public void CancelInteraction()
        {
            if (_notification != null)
            {
                
                _notification.Confirmed = false;
            }

            FinishInteraction();
        }
    }
}
