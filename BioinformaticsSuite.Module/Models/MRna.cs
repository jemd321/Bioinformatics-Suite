using BioinformaticsSuite.Module.Enums;

namespace BioinformaticsSuite.Module.Models
{
    public class MRna : LabelledSequence
    {
        public MRna(string label, string sequence) : base(label, sequence)
        {
            SequenceType = SequenceType.MRna;
        }
    }
}