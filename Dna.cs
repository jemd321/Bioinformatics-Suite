using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Rosalind_Locating_Restriction_Sites
{
    public class Dna
    {
        // In general it is better to have a class that takes a constructor then one that makes a new object and then manually set propertites. Can be Ok but usually for "optional" properties.
        public Dna(string sequence)
        {
            this.Sequence = sequence;
        }

        // Making this a FileInfo as already used single string signature above for sequence. Can't have same signature twice
        public Dna(FileInfo fileInfo)
        {
            string sequence = File.ReadAllText(fileInfo.FullName);
            this.Sequence = string.IsNullOrEmpty(sequence) ? string.Empty : Regex.Replace(sequence, @"\s", "");
        }

        public string Sequence { get; set; }

        public string ReversedComplement => ReverseComplement(this.Sequence);

        public string Complement => FindComplement(this.Sequence);

        public string ReversedSequence => ReverseSequence(this.Sequence);

        //public List<string> FindRestricitionSites()
        //{
          //  RestrictionSiteHelper r = new RestrictionSiteHelper(this.Sequence, ReverseComplement);
       // } 
            
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
