using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsSuite.Module.Services
{
    public class ParsingErrorMessage
    {
        public ParsingErrorMessage(int lineNumber, string errorDescription)
        {
            LineNumber = lineNumber;
            ErrorDescription = errorDescription;
        }

        public int LineNumber { get; private set; }
        public string ErrorDescription { get; private set; }
    }
}
