using BioinformaticsSuite.Module.Enums;

namespace BioinformaticsSuite.Module.Models
{
    public class Rna : LabelledSequence
    {
        public Rna(string label, string sequence) : base(label, sequence)
        {
            SequenceType = SequenceType.Rna;
        }
    }
}