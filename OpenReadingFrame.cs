using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Bioinformatics_Suite
{
    public class OpenReadingFrame
    {
        private StringBuilder labelBuilder = new StringBuilder();

        public OpenReadingFrame(Protein protein)
        {
            this.Sequence = protein.Sequence;
            this.Label = protein.Label;
            OrfList = FindOpenReadingFrames(Sequence);
        }

        public string Label { get; }
        public string Sequence { get; }
        public Dictionary<string, string> OrfList { get; }

        private Dictionary<string, string> FindOpenReadingFrames(string proteinSequence)
        {
            var openReadingFrames = new Dictionary<string, string>();
            Regex orfMatcher = new Regex("M[^X]*X");

            Match match = orfMatcher.Match(proteinSequence);

            while (match.Success)
            {
                string foundOrf = TrimOrf(match.Value);
                if (foundOrf != string.Empty)
                {
                    string orfLabel = BuildLabel(match);
                    openReadingFrames.Add(orfLabel, foundOrf);
                }             
                match = orfMatcher.Match(proteinSequence, match.Index + 1);
            }
            return openReadingFrames;
        }

        private static string TrimOrf(string untrimmedOrf)
        {
            return untrimmedOrf.TrimEnd('X');
        }

        private string BuildLabel(Match match)
        {
            int startIndex = match.Index + 1;
            int endIndex = startIndex + match.Length - 1;

            labelBuilder.Append(Label + " " + startIndex + "-" + endIndex);
            string orfLabel = labelBuilder.ToString();
            labelBuilder.Clear();
            return orfLabel;

        }
    }
}
