using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Utility;
using NuGet;

namespace BioinformaticsSuite.Module.Models
{
    public interface IGenbankConverter
    {
        Dictionary<string, string> ConvertGenbankFastaDna(string genbankFile);
        Dictionary<string, string> ConvertGenbankFastaProtein(string genbankFile);
    }

    public class GenbankConverter : IGenbankConverter
    {
        // Regex for splitting a genbank file into single records
        private static readonly Regex FileSeparatorRegex = new Regex(@"(?<!http:)\/\/");

        // Regexes for finding index of keywords that define section info
        private static readonly Regex VersionRegex = new Regex(@"VERSION", RegexOptions.Compiled);
        private static readonly Regex KeywordsRegex = new Regex(@"KEYWORDS", RegexOptions.Compiled);
        private static readonly Regex DefinitionRegex = new Regex(@"DEFINITION", RegexOptions.Compiled);
        private static readonly Regex AccessionRegex = new Regex(@"ACCESSION", RegexOptions.Compiled);
        private static readonly Regex OriginRegex = new Regex(@"ORIGIN", RegexOptions.Compiled);

        // Regexes for removing various delimiters from Genbank labels and sequences
        private static readonly Regex NewlineRegex = new Regex(@"[\n\r]", RegexOptions.Compiled);
        private static readonly Regex LineNumberRegex = new Regex(@"[\s0-9/]");
        private static readonly Regex LabelWhitespaceRegex = new Regex(@"(?<= ) ");

        private static readonly StringBuilder RecordBuilder = new StringBuilder();

        public Dictionary<string, string> ConvertGenbankFastaDna(string genbankFile)
        {
            var labelledSequences = new Dictionary<string, string>();
            List<string> genbankRecords = SplitGenbankFile(genbankFile);
            foreach (var record in genbankRecords)
            {
                string label = ExtractLabel(record);
                string sequence = ExtractSequence(record);
                labelledSequences.Add(label, sequence);
            }
            return labelledSequences;
        }

        public Dictionary<string, string> ConvertGenbankFastaProtein(string genbankFile)
        {
            var labelledSequences = new Dictionary<string, string>();
            List<string> genbankRecords = SplitGenbankFile(genbankFile);
            foreach (var record in genbankRecords)
            {
                string label = ExtractLabel(record);
                string sequence = ExtractSequence(record);
                labelledSequences.Add(label, Translation.TranslateDnaToProtein(sequence));
            }
            return labelledSequences;
        }

        private List<string> SplitGenbankFile(string genbankFile)
        {
            if (!genbankFile.EndsWith("//"))
            {
                // FORMAT ERROR - not a valid genbank file
            }
            List<string> genbankRecords = ParseGenbank(genbankFile).ToList();
            string last = genbankRecords.Last();
            if (string.IsNullOrWhiteSpace(last))
            {
                genbankRecords.RemoveAt(genbankRecords.Count - 1);
            }
            else
            {
                // FORMAT ERROR - Inform user
            }
            return genbankRecords;
        }

        private string[] ParseGenbank(string genbankRecord)
        {
            string[] genbankRecords = FileSeparatorRegex.Split(genbankRecord);
            return genbankRecords;

        }

        private string ExtractLabel(string record)
        {
            // Finds start and end index of VERSION section (+7 to remove tag from resulting label)
            int versionIndex = VersionRegex.Match(record).Index + 7;
            int keywordsIndex = KeywordsRegex.Match(record).Index;

            string version = ExtractVersion(record, versionIndex, keywordsIndex);

            // Finds start and end index of DESCRIPTION section (+10 to remove tag from resulting label)
            int descriptionIndex = DefinitionRegex.Match(record).Index + 10;
            int accessionIndex = AccessionRegex.Match(record).Index;

            string description = ExtractDescription(record, descriptionIndex, accessionIndex);

            // Build Fasta Label
            string label = RecordBuilder.Append(">").Append(version).Append(" ").Append(description).ToString();
            label = LabelWhitespaceRegex.Replace(label, "");
            RecordBuilder.Clear();
            return label;
        }

        private string ExtractSequence(string record)
        {
            // Find start index and end index of ORIGIN section
            int originIndex = OriginRegex.Match(record).Index + 6;
            int length = record.Length - originIndex;

            string sequence = record.Substring(originIndex, length);
            sequence = LineNumberRegex.Replace(sequence, "").ToUpper();
            return sequence.Trim(' ');
        }

        private string ExtractVersion(string record, int startIndex, int endIndex)
        {
            int length = endIndex - startIndex;
            string version = record.Substring(startIndex, length);
            version = NewlineRegex.Replace(version, "");
            return version.Trim(' ');
        }

        private string ExtractDescription(string record, int startIndex, int endIndex)
        {
            int length = endIndex - startIndex;
            string description = record.Substring(startIndex, length);
            description = NewlineRegex.Replace(description, "");
            return description.Trim(' ');
        }
    }
}
