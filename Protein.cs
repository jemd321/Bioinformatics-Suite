using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Bioinformatics_Suite
{
    public class Protein : LabelledSequence
    {
        public Protein(string label, string sequence) : base(label, sequence) {}

        public Protein(Dna dna) : base(dna) {}

    }
}