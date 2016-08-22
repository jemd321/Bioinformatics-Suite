using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Bioinformatics_Suite
{
    public class Sequence
    {
        public Sequence(InputParser inputParser)
        {
            this.Sequences = inputParser.GetParsedSequences;
        }

        public Dictionary<string, string> Sequences { get; }
        public Dictionary<string, string> ReversedSequences { get; }

        public Dictionary<string, string> ReverseSequences(Dictionary<string, string> Sequences)
        {
            foreach (KeyValuePair<string, string> KVpair in Sequences)
            {
                //set the value by looking up the key for that string and then adding it to the dictionary again.
                // this should overide since it's a duplicate key. Double check this.
            }   
        }

        // Add methods that ccan apply to both DNA and Protein here. For example, reversing the sequence.
        // Add specific methods to each of the sub classes, eg. DNA will have a method that returns reverse complement,
        // whereas, Protein may have a method that calculates the molecular weight of the protein.

    

    }
}