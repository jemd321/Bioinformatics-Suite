using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BioinformaticsSuite.Module.Services
{
    public interface IEmblParser
    {
        List<string> EmblRecords { get; }
        string ErrorMessage { get; }
        bool TryParseEmblFile(string emblFile);
    }

    public class EmblParser : IEmblParser
    {
        private static readonly Regex FileSeparatorRegex = new Regex(@"(?<!http:)\/\/", RegexOptions.Compiled);

        public List<string> EmblRecords { get; private set; }
        public string ErrorMessage { get; private set; }

        public bool TryParseEmblFile(string emblFile)
        {
            if (!emblFile.EndsWith("//"))
            {
                ErrorMessage = "Invalid Embl format - each record must end with '//'";
                return false;
            }
            EmblRecords = ParseEmbl(emblFile).ToList();
            string last = EmblRecords.Last();
            if (string.IsNullOrWhiteSpace(last))
            {
                EmblRecords.RemoveAt(EmblRecords.Count - 1);
            }
            else
            {
                ErrorMessage = "Invalid Embl format";
                return false;
            }
            return true;
        }

        private static IEnumerable<string> ParseEmbl(string emblRecord)
        {
            string[] genbankRecords = FileSeparatorRegex.Split(emblRecord);
            return genbankRecords;
        }
    }
}
