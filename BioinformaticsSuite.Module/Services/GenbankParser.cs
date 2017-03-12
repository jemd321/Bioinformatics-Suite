using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BioinformaticsSuite.Module.Services
{
    public interface IGenbankParser
    {
        List<string> GenbankRecords { get; }
        string ErrorMessage { get; }
        bool TryParseGenbankFile(string genbankFile);
    }

    public class GenbankParser : IGenbankParser
    {
        private static readonly Regex FileSeparatorRegex = new Regex(@"(?<!http:)\/\/", RegexOptions.Compiled);

        public List<string> GenbankRecords { get; private set; }
        public string ErrorMessage { get; private set; }

        public bool TryParseGenbankFile(string genbankFile)
        {
            var whitespaceChars = new char[] {' ', '\n', '\r'};
            genbankFile = genbankFile.TrimEnd(whitespaceChars);

            if (!genbankFile.EndsWith("//"))
            {
                ErrorMessage = "Invalid Genbank format - each record must end with '//'";
                return false;
            }

            GenbankRecords = ParseGenbank(genbankFile).ToList();
            string last = GenbankRecords.Last();
            if (string.IsNullOrWhiteSpace(last))
            {
                GenbankRecords.RemoveAt(GenbankRecords.Count - 1);
            }
            else
            {
                ErrorMessage = "Invalid Genbank format";
                return false;
            }
            return true;
        }

        private static IEnumerable<string> ParseGenbank(string genbankRecord)
        {
            string[] genbankRecords = FileSeparatorRegex.Split(genbankRecord);
            return genbankRecords;
        }
    }
}
