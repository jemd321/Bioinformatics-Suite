using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Utility;

namespace BioinformaticsSuite.Module.Models
{
    public class Dna : LabelledSequence
    {
        private string _complement;
        private string _reverseComplement;

        public Dna(string label, string sequence) : base(label, sequence)
        {
            SequenceType = SequenceType.Dna;
        }

        public string Complement
        {
            get
            {
                if (_complement == null)
                {
                    return _complement = FindComplement(this.Sequence);
                }
                else return _complement;
            }
        }

        public string ReverseComplement
        {
            get
            {
                if (_reverseComplement == null)
                {
                    return _reverseComplement = FindReverseComplement(ReversedSequence);
                }
                else return _reverseComplement;
            }
        }

        private string FindComplement(string sequence)
        {
            return sequence.Complement();
        }

        private string FindReverseComplement(string reversedSequence)
        {
            return reversedSequence.Complement();
        }
    }
}
