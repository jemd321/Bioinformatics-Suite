namespace BioinformaticsSuite.Module.Models
{
    public abstract class LabelledSequence
    {
        private string _reversedSequence;

        protected LabelledSequence(string label, string sequence)
        {
            Label = label;
            Sequence = sequence;
        }

        protected LabelledSequence(Dna dna)
        {
            Label = dna.Label;
            Sequence = dna.Sequence;
            Sequence = Translate(Sequence);
        }

        public string Label { get; }
        public string Sequence { get; }

        public string ReversedSequence
        {
            get
            {
                if (_reversedSequence == null)
                {
                    return _reversedSequence = ReverseSequence(this.Sequence);
                }
                else return _reversedSequence;
            }
        }

        private string ReverseSequence(string sequence)
        {
            return sequence.Reverse();
        }

        private string Translate(string dnaSequence)
        {
            var protein = Translator.TranslateDna(dnaSequence);
            return protein;
        }
    }
}