using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;

namespace BioinformaticsSuite.Module.Services
{
    public class ValidationErrorMessage
    {
        public ValidationErrorMessage(SequenceType expectedSequenceType, string label, int errorIndex, string errorContent, string errorDescription)
        {
            ExpectedSequenceType = expectedSequenceType;
            Label = label;
            ErrorIndex = errorIndex;
            ErrorContent = errorContent;
            ErrorDescription = errorDescription;
        }

        public SequenceType ExpectedSequenceType { get; private set; }
        public string Label { get; private set; }
        public int ErrorIndex { get; private set; }
        public string ErrorContent { get; private set; }        
        public string ErrorDescription { get; private set; }
    }
}
