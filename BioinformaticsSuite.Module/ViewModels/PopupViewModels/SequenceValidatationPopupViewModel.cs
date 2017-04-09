using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BioinformaticsSuite.Module.Popups;
using BioinformaticsSuite.Module.Services;
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
            OkCommand = new DelegateCommand(OkInteraction);
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

        public ValidationErrorMessage ErrorMessage { get; set; }
        
        public ICommand OkCommand { get; private set; }

        public void OkInteraction()
        {
            FinishInteraction();
        }
    }
}
