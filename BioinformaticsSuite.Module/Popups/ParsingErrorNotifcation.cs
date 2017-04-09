using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Services;
using Prism.Interactivity.InteractionRequest;

namespace BioinformaticsSuite.Module.Popups
{
    public class ParsingErrorNotifcation : Notification
    {
        public ParsingErrorNotifcation(ParsingErrorMessage errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public ParsingErrorMessage ErrorMessage { get; set; }
    }
}
