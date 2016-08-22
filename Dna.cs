using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Bioinformatics_Suite
{
    public class Dna : Sequence
    {
        // Constructors

        public Dna(InputParser inputParser) : base(inputParser) { }

        //properties

        public string ReversedComplement => ReverseComplement(this.Sequences);

        public string Complement => FindComplement(this.Sequences);

        public string ReversedSequence => ReverseSequence(this.Sequences);

            //Methods
            
        private static string ReverseSequence(string sequence)
        {
            return sequence.Reverse();
        }

        private static string FindComplement(string sequence)
        {
            return sequence.Complement();
            Dna dna = new Dna(inputParser);
            dna.Sequences
        }

        private static string ReverseComplement(string sequence)
        {
            return sequence.Reverse().Complement();
        }

    }
}
