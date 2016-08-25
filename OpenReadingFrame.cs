using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bioinformatics_Suite
{
    public class OpenReadingFrame
    {
        private string sequence;
        private string label;

        public OpenReadingFrame(Protein protein)
        {
            this.sequence = protein.Sequence;
            this.label = protein.Label;
            OrfList = FindOpenReadingFrames(sequence);
        }

        public List<string> OrfList { get; }

        private static List<string> FindOpenReadingFrames(string proteinSequence)
        {
            List<string> openReadingFrames = new List<string>();
            Regex orfMatcher = new Regex("M[^X]*X");
          
                Match match = orfMatcher.Match(proteinSequence);
                while (match.Success)
                {
                    string foundOrf = TrimOrf(match.Value);
                    if (foundOrf != string.Empty && !openReadingFrames.Contains(foundOrf))
                    {
                        openReadingFrames.Add(foundOrf);
                    }
                    match = match.NextMatch();
                }          
            return openReadingFrames;
        }

        private static string TrimOrf(string untrimmedOrf)
        {
            return untrimmedOrf.TrimEnd('X');
        }
    }
}
