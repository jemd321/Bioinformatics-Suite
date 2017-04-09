using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Services;
using Prism.Interactivity.InteractionRequest;

namespace BioinformaticsSuite.Module.Popups
{
    public class SequenceValidationNotification : Notification
    {
        public SequenceValidationNotification(ValidationErrorMessage errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public ValidationErrorMessage ErrorMessage { get; private set; }

    }
}
