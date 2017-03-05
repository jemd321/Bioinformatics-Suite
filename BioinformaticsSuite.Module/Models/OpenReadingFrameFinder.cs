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
        private string _sequenceLabel;
        private readonly IReadingFrameFactory _readingFrameFactory;

        public OpenReadingFrameFinder(IReadingFrameFactory readingFrameFactory)
        {
            this._readingFrameFactory = readingFrameFactory;
        }
        public bool FoundOrf { get; private set; }

        public Dictionary<string, string> FindOpenReadingFrames(Dna dna)
        {
            _sequenceLabel = dna.Label;
            var labelledOrfs = new Dictionary<string, string>();
            var orfMatcher = new Regex("M[^X]*X", RegexOptions.Compiled);

            ReadingFrame readingFrame = _readingFrameFactory.GetReadingFrames(dna);
            foreach (var labelledFrame in readingFrame.LabelledFrames)
            {
                var frame = Translation.TranslateDnaToProtein(labelledFrame.Value);
                // Must use a while loop here to ensure that the regex matches overlapping ORFs
                Match match = orfMatcher.Match(frame);
                while (match.Success)
                {
                    FoundOrf = true;
                    string orf = TrimOrf(match.Value);
                    if (orf != string.Empty)
                    {
                        string orfLabel = BuildLabel(match);
                        labelledOrfs.Add(orfLabel, orf);
                    }
                    match = orfMatcher.Match(frame, match.Index + 1);
                }
            }
            return labelledOrfs;
        }      

        // Remove stop codon
        private static string TrimOrf(string untrimmedOrf)
        {
            return untrimmedOrf.TrimEnd('X');
        }

        private string BuildLabel(Match match)
        {
            int startIndex = match.Index + 1;
            int endIndex = startIndex + match.Length - 1;

            _labelBuilder.Append(_sequenceLabel)
                .Append(": ")
                .Append(startIndex)
                .Append("-")
                .Append(endIndex)
                .Append(" Length: ")
                .Append(match.Length - 1);
            string orfLabel = _labelBuilder.ToString();
            _labelBuilder.Clear();
            return orfLabel;
        }
    }
}
