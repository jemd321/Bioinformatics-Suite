using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsSuite.Module.Services
{
    public class ParsingErrorMessage
    {
        public ParsingErrorMessage(string lineNumber, string errorDescription)
        {
            LineNumber = lineNumber;
            ErrorDescription = errorDescription;
        }

        public string LineNumber { get; private set; }
        public string ErrorDescription { get; private set; }
    }
}
