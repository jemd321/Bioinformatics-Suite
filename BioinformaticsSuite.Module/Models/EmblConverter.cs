using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Utility;

namespace BioinformaticsSuite.Module.Models
{
    public interface IEmblConverter
    {
        Dictionary<string, string> ConvertEmblFastaDna(List<string> emblRecord);
        Dictionary<string, string> ConvertEmblFastaProtein(List<string> emblRecord);
    }

    public class EmblConverter : IEmblConverter
    {
        private static readonly Regex AccessionRegex = new Regex(@"AC.*?;", RegexOptions.Compiled);
        private static readonly Regex DescriptionRegex = new Regex(@"DE.*", RegexOptions.Compiled);
        private static readonly Regex SequenceRegex = new Regex(@"SQ.*;", RegexOptions.Compiled);

        private static readonly Regex SequenceReplaceRegex = new Regex(@"[;\s\d\/]", RegexOptions.Compiled);

        private static readonly StringBuilder RecordBuilder = new StringBuilder();

        public Dictionary<string, string> ConvertEmblFastaDna(List<string> emblRecords)
        {
            var labelledFastas = new Dictionary<string, string>();
            foreach (string emblRecord in emblRecords)
            {
                string label = ExtractLabel(emblRecord);
                string sequence = ExtractSequence(emblRecord);
                labelledFastas.Add(label, sequence.ToUpper());
            }
            return labelledFastas;
        }

        public Dictionary<string, string> ConvertEmblFastaProtein(List<string> emblRecords)
        {
            var labelledFastas = new Dictionary<string, string>();
            foreach (string emblRecord in emblRecords)
            {
                string label = ExtractLabel(emblRecord);
                string sequence = ExtractSequence(emblRecord).ToUpper();
                labelledFastas.Add(label, Translation.TranslateDnaToProtein(sequence));
            }
            return labelledFastas;
        }

        private static string ExtractLabel(string emblRecord)
        {
            var trimAcChars = new char[] {' ', 'A', 'C', ';', '\r', '\n'};
            string accession = AccessionRegex.Match(emblRecord).Value.Trim(trimAcChars);

            var trimDeChars = new char[] {' ', 'D', 'E', ';', '\r', '\n'};
            string description = DescriptionRegex.Match(emblRecord).Value.Trim(trimDeChars);

            // >ACCESSION|DESCRIPTION - label format
            string label = RecordBuilder.Append(">").Append(accession).Append("|").Append(description).ToString();
            RecordBuilder.Clear();
            return label;
        }

        private static string ExtractSequence(string emblRecord)
        {
            var sequenceHeader = SequenceRegex.Match(emblRecord);
            var headerIndex = sequenceHeader.Index;
            var headerLength = sequenceHeader.Length;
            var sequenceStartIndex = headerIndex + headerLength;

            var sequenceLength = emblRecord.Length - sequenceStartIndex;
            string sequence = emblRecord.Substring(sequenceStartIndex, sequenceLength);
            return SequenceReplaceRegex.Replace(sequence, "");
        }
    }



}
