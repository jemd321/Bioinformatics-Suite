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
    class ParsingErrorPopupViewModel : BindableBase, IInteractionRequestAware
    {
        private ParsingErrorNotifcation _notification;

        public ParsingErrorPopupViewModel()
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
                if (!(value is ParsingErrorNotifcation)) return;
                _notification = (ParsingErrorNotifcation)value;
                OnPropertyChanged(() => Notification);
            }
        }

        public ParsingErrorMessage ErrorMessage { get; set; }

        public ICommand OkCommand { get; private set; }

        public void OkInteraction()
        {
            FinishInteraction();
        }
    }
}
