using System.IO;
using System.Text.RegularExpressions;

namespace Bioinformatics_Suite
{
    public class Protein : Sequence
    {
        public Protein(string sequence) : base(sequence) { }

        public Protein(FileInfo fileInfo) : base (fileInfo) { }

        public string Sequence { get; set; }

    }
}