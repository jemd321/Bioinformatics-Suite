using System.Collections.Generic;
using Bioinformatics_Suite;

namespace Bioinformatics_Suite
{
    public class ReadingFrame
    {
        /*   - This class splits one DNA sequence into 6 reading frames, using the help of the DNA Converter.
             - Three are forward and three are made from the reverse complement of the forward string.
             - Reverse frames are the 'reverse complement', ie. the forward string reversed and then each base 
               switched to its opposite base pair eg. A -> T , G -> C and vice-versa. 
             - eg. "ACTGCTA" would be f1 "ACTGCTA" f2 "CTGCTA" f3 "TGCTA" | r1 "TAGCAGT" r2 "AGCAGT" r3 "GCACT"
        */

            // For reading frame, rather than return a list of frames, have one reading frame object per labelled sequence containing
            // a dictionary with each of the 6 frames labelled with the AccessionNo + the reading frame, eg. >AAG3RAF_Frame+1

        public ReadingFrame(Dna dna)
        {
            this.Dna = dna;
            this.Label = dna.Label;
            this.LabelledFrames = SplitIntoFrames(dna.Sequence);
        }

        public Dna Dna;
        public string Label;
        public Dictionary<string, string> LabelledFrames { get; }

        private Dictionary<string, string> SplitIntoFrames(string sequence)
        {
            // Each reading frame is trimmed if needed to ensure a multiple of 3,
            // so that it forms a list of codons with no useless bases.

            string forward1 = sequence;
            var remainder = forward1.Length%3;
            if (remainder != 0)
            {
                forward1 = TrimBases(forward1, remainder);
            }

            string forward2 = sequence.Substring(1);
            remainder = forward2.Length%3;
            if (remainder != 0)
            {
                forward2 = TrimBases(forward2, remainder);
            }

            string forward3 = sequence.Substring(2);
            remainder = forward3.Length%3;
            if (remainder != 0)
            {
                forward3 = TrimBases(forward3, remainder);
            }

            string reverseComplement = Dna.ReverseComplement;

            string reverse1 = reverseComplement;
            remainder = reverse1.Length%3;
            if (remainder != 0)
            {
                reverse1 = TrimBases(reverse1, remainder);
            }

            string reverse2 = reverseComplement.Substring(1);
            remainder = reverse2.Length%3;
            if (remainder != 0)
            {
                reverse2 = TrimBases(reverse2, remainder);
            }

            string reverse3 = reverseComplement.Substring(2);
            remainder = reverse3.Length%3;
            if (remainder != 0)
            {
                reverse3 = TrimBases(reverse3, remainder);
            }

            Dictionary<string, string> labelledFrames = new Dictionary<string, string>()
            {
                {Label + " +1", forward1},
                {Label + " +2", forward2},
                {Label + " +3", forward3},
                {Label + " -1", reverse1},
                {Label + " -2", reverse2},
                {Label + " -3", reverse3},
            };
            
            return labelledFrames;
        }

        private static string TrimBases(string inputDna, int numberBasesToBeTrimmed)
        {
            var length = inputDna.Length;
            var removalIndex = length - numberBasesToBeTrimmed;

            var trimmedDna = inputDna.Remove(removalIndex, numberBasesToBeTrimmed);
            return trimmedDna;
        }
    }
}