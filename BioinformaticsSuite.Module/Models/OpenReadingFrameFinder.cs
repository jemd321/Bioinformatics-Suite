using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using BioinformaticsSuite.Module.Utility;

namespace BioinformaticsSuite.Module.Models
{
    public interface IOpenReadingFrameFinder
    {
        Dictionary<string, string> FindOpenReadingFrames(Dna dna);
    }

    public class OpenReadingFrameFinder : IOpenReadingFrameFinder
    {
        private readonly StringBuilder _labelBuilder = new StringBuilder();
        private readonly IReadingFrameFactory _readingFrameFactory;
        private readonly Regex _orfMatcher = new Regex(@"M[^*]*\*", RegexOptions.Compiled);

        private string _sequenceLabel;

        public OpenReadingFrameFinder(IReadingFrameFactory readingFrameFactory)
        {
            _readingFrameFactory = readingFrameFactory;
        }

        public bool FoundOrf { get; private set; }

        public Dictionary<string, string> FindOpenReadingFrames(Dna dna)
        {
            _sequenceLabel = dna.Label;
            var labelledOrfs = new Dictionary<string, string>();

            var readingFrame = _readingFrameFactory.GetReadingFrames(dna);
            foreach (var labelledFrame in readingFrame.LabelledFrames)
            {
                var frame = Translation.TranslateDnaToProtein(labelledFrame.Value);
                // Must use a while loop here to ensure that the regex matches overlapping ORFs
                Match match = _orfMatcher.Match(frame);
                while (match.Success)
                {
                    FoundOrf = true;
                    string orf = TrimStopCodon(match.Value);
                    if (orf != string.Empty)
                    {
                        string orfLabel = BuildLabel(match);
                        labelledOrfs.Add(orfLabel, orf);
                    }
                    match = _orfMatcher.Match(frame, match.Index + 1);
                }
            }
            return labelledOrfs;
        }

        private static string TrimStopCodon(string untrimmedOrf)
        {
            return untrimmedOrf.TrimEnd('*');
        }

        // Label consists of the length (start-end) followed by the sequence
        private string BuildLabel(Match match)
        {
            // + 1 to convert to non-0 based index
            int startIndex = match.Index + 1;
            // -2 to account for the trimmed stop codon
            int endIndex = startIndex + match.Length - 2;

            _labelBuilder.Append(_sequenceLabel)
                .Append(": ")
                .Append(startIndex)
                .Append("-")
                .Append(endIndex)
                .Append(" length: ")
                .Append(match.Length - 1);
            string orfLabel = _labelBuilder.ToString();
            _labelBuilder.Clear();
            return orfLabel;
        }
    }
}