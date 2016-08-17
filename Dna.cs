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

        public Dna(string parsedSequence) : base(parsedSequence) { }

        public Dna(Dictionary<string, string> fastaSequences) : base (fastaSequences) { }

        //properties

        public string Sequence { get; }

        public string ReversedComplement => ReverseComplement(this.Sequence);

        public string Complement => FindComplement(this.Sequence);

        public string ReversedSequence => ReverseSequence(this.Sequence);

            //Methods
            
        private static string ReverseSequence(string sequence)
        {
            return sequence.Reverse();
        }

        private static string FindComplement(string sequence)
        {
            return sequence.Complement();
        }

        private static string ReverseComplement(string sequence)
        {
            return sequence.Reverse().Complement();
        }

    }
}
