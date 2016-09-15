namespace BioinformaticsSuite.Module.Models
{
    public class Dna : LabelledSequence, IDna
    {
        private string _complement;
        private string _reverseComplement;

        public Dna(string label, string sequence) : base(label, sequence)  { }

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
                    return _reverseComplement = FindReverseComplement(this.ReversedSequence);
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

    public interface IDna
    {
        string Label { get; }
        string Sequence { get; }
    }
}
