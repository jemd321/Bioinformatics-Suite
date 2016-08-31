using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Bioinformatics_Suite
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