using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Bioinformatics_Suite
{
    public class Protein : Sequence
    {
        public Protein(string parsedSequence) : base(parsedSequence) { }

        public Protein(Dictionary<string, string> fastaSeqeunnces) : base (fastaSeqeunnces) { }

        public string parsedSequence { get; }

    }
}