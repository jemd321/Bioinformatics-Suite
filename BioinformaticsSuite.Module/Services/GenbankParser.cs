﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BioinformaticsSuite.Module.Services
{
    public interface IGenbankParser
    {
        List<string> GenbankRecords { get; }
        string ErrorMessage { get; }
        bool TryParseGenbankFile(string genbankFile);
        void ResetSequences();
    }

    public class GenbankParser : IGenbankParser
    {
        // Identifies '//' that indicates end of record
        private static readonly Regex FileSeparatorRegex = new Regex(@"(?<!http:)\/\/", RegexOptions.Compiled);

        public List<string> GenbankRecords { get; private set; }
        public string ErrorMessage { get; private set; } = string.Empty;

        public bool TryParseGenbankFile(string genbankFile)
        {
            var whitespaceChars = new[] {' ', '\n', '\r'};
            genbankFile = genbankFile.TrimEnd(whitespaceChars);

            if (!genbankFile.EndsWith("//"))
            {
                ErrorMessage = "Invalid Genbank format - each record must end with '//'";
                return false;
            }

            GenbankRecords = ParseGenbank(genbankFile).ToList();

            // Regex split generates an empty string as the last record
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

        public void ResetSequences()
        {
            ErrorMessage = "";
            GenbankRecords = new List<string>();
        }

        private static IEnumerable<string> ParseGenbank(string genbankRecord)
        {
            string[] genbankRecords = FileSeparatorRegex.Split(genbankRecord);
            return genbankRecords;
        }
    }
}