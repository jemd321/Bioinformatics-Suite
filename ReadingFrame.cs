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

        public List<string> Frames { get; }

        public ReadingFrame(Dna dna)
        {
            this.Frames = SplitIntoFrames(dna.Sequence, dna.ReversedComplement);
        }

        private static List<string> SplitIntoFrames(string dna, string reverseComplementDna)
        {
            // Each reading frame is trimmed if needed to ensure a multiple of 3,
            // so that it forms a list of codons with no useless bases.


            string forward1 = dna;
            var remainder = forward1.Length%3;
            if (remainder != 0)
            {
                forward1 = TrimBases(forward1, remainder);
            }

            string forward2 = dna.Substring(1);
            remainder = forward2.Length%3;
            if (remainder != 0)
            {
                forward2 = TrimBases(forward2, remainder);
            }

            string forward3 = dna.Substring(2);
            remainder = forward3.Length%3;
            if (remainder != 0)
            {
                forward3 = TrimBases(forward3, remainder);
            }

            string reverse1 = reverseComplementDna;
            remainder = reverse1.Length%3;
            if (remainder != 0)
            {
                reverse1 = TrimBases(reverse1, remainder);
            }

            string reverse2 = reverseComplementDna.Substring(1);
            remainder = reverse2.Length%3;
            if (remainder != 0)
            {
                reverse2 = TrimBases(reverse2, remainder);
            }

            string reverse3 = reverseComplementDna.Substring(2);
            remainder = reverse3.Length%3;
            if (remainder != 0)
            {
                reverse3 = TrimBases(reverse3, remainder);
            }

            return new List<string>
            {
                forward1,
                forward2,
                forward3,
                reverse1,
                reverse2,
                reverse3
            };
        }

        private static string TrimBases(string inputDna, int numberBasesToBeTrimmed)
        {
            int length = inputDna.Length;
            int removalIndex = length - numberBasesToBeTrimmed;

            string trimmedDna = inputDna.Remove(removalIndex, numberBasesToBeTrimmed);
            return trimmedDna;
        }
    }
}