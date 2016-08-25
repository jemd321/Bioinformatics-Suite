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
            this.Sequences = inputParser.ParsedSequences;
        }

        public Dictionary<string, string> Sequences { get; }

        private Dictionary<string, string> reversedSequences;

        public Dictionary<string, string> ReversedSequences
        {
            get
            {
                ReverseSequences(this.Sequences);
                return reversedSequences;
            }
        }
        
        public void ReverseSequences(Dictionary<string, string> forwardSequences)
        {
            foreach (KeyValuePair<string, string> sequencePair in forwardSequences)
            {
                string sequence = sequencePair.Value;
                string reversedSequence = sequence.Reverse();
                reversedSequences[sequencePair.Key] = reversedSequence;
            }
       }
    }
}