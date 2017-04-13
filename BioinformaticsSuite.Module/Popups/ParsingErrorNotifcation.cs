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
            LineNumber = errorMessage.LineNumber;
            ErrorDescription = errorMessage.ErrorDescription;
        }

        public int LineNumber { get; private set; }
        public string ErrorDescription { get; private set; }
    }
}
