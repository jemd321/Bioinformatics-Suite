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
        public Sequence(string parsedSequence)
        {
            this.ParsedSequence = parsedSequence;
        }

        public Sequence(Dictionary<string,string> fastaSeqeuences)
        {
            this.FastaSequences = fastaSeqeuences;
        }

        public string ParsedSequence { get; }
        public Dictionary<string, string> FastaSequences { get; }

    }
}