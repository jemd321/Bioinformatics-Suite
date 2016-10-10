using BioinformaticsSuite.Module.Enums;

namespace BioinformaticsSuite.Module.Models
{
    public class Protein : LabelledSequence
    {
        public Protein(string label, string sequence) : base(label, sequence)
        {
            SequenceType = SequenceType.Protein;
        }
    }
}