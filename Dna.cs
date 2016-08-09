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

        public Dna(string sequence) : base(sequence) { }

        public Dna(FileInfo fileInfo) : base (fileInfo) { }

        //properties

        public string Sequence { get; set; }

        public string ReversedComplement => ReverseComplement(this.Sequence);

        public string Complement => FindComplement(this.Sequence);

        public string ReversedSequence => ReverseSequence(this.Sequence);

        //public List<string> FindRestricitionSites()
        //{
          //  RestrictionSiteHelper r = new RestrictionSiteHelper(this.Sequence, ReverseComplement);
       // } 

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
