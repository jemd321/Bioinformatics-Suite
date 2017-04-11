using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Services;
using Prism.Interactivity.InteractionRequest;

namespace BioinformaticsSuite.Module.Popups
{
    public class SequenceValidationNotification : Notification
    {
        /*
        private SequenceType _expectedSequenceType;
        private string _label;
        private int _errorIndex;
        private string _errorContent;
        private string _errorDescription;
        */
        public SequenceValidationNotification(ValidationErrorMessage errorMessage)
        {
            ExpectedSequenceType = errorMessage.ExpectedSequenceType;
            Label = errorMessage.Label;
            ErrorIndex = errorMessage.ErrorIndex;
            ErrorContent = errorMessage.ErrorContent;
            ErrorDescription = errorMessage.ErrorDescription;
        }

        public SequenceType ExpectedSequenceType { get; private set; }
        public string Label { get; private set; }
        public int ErrorIndex { get; private set; }
        public string ErrorContent { get; private set; }
        public string ErrorDescription { get; private set; }

        /*
        public SequenceType ExpectedSequenceType
        {
            get { return _expectedSequenceType; }
            set { SetProperty(ref _expectedSequenceType, value); }
        }

        public string Label
        {
            get { return _label; }
            set { SetProperty(ref _label, value); }
        }

        public int ErrorIndex
        {
            get { return _errorIndex; }
            set { SetProperty(ref _errorIndex, value); }
        }

        public string ErrorContent
        {
            get { return _errorContent; }
            set { SetProperty(ref _errorContent, value); }
        }

        public string ErrorDescription
        {
            get { return _errorDescription; }
            set { SetProperty(ref _errorDescription, value); }
        }
        */
    }
}
