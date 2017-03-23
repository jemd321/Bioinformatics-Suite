using System.Collections.Generic;
using System.Text;

namespace BioinformaticsSuite.Module.Models
{
    public interface IFastaManipulator
    {
        Dictionary<string, string> CombineFasta(List<LabelledSequence> sequences);
        Dictionary<string, string> SplitFasta(LabelledSequence sequence, int fragmentLength);
    }


    public class FastaManipulator : IFastaManipulator
    {
        private static readonly StringBuilder SequenceBuilder = new StringBuilder();

        // Combine two or more FASTA format sequences into a single FASTA sequence
        public Dictionary<string, string> CombineFasta(List<LabelledSequence> sequences)
        {
            int sequenceCount = sequences.Count;
            foreach (var labelledSequence in sequences)
            {
                string sequence = labelledSequence.Sequence;
                SequenceBuilder.Append(sequence);
            }
            string combinedSequence = SequenceBuilder.ToString();
            int sequenceLength = combinedSequence.Length;

            // Label format eg. >100 base sequence from 4 sequences
            string combinedLabel = ">" + sequenceLength + " base sequence from " + sequenceCount + " sequences";
            SequenceBuilder.Clear();
            return new Dictionary<string, string> {{combinedLabel, combinedSequence}};
        }

        // Split one FASTA sequence into multiple sequences based on a user supplied desired fragment length
        public Dictionary<string, string> SplitFasta(LabelledSequence labelledSequence, int fragmentLength)
        {
            var splitSequences = new Dictionary<string, string>();

            string sequence = labelledSequence.Sequence;
            int originalLength = labelledSequence.Sequence.Length;
            int fragmentNumber = 1;

            for (int i = 0; i < originalLength; i += fragmentLength)
            {
                // Label format : >Fragment Number;Source_label;Start Index;End Index;Fragment Length;Original Length;
                SequenceBuilder.Append(">fragment_")
                    .Append(fragmentNumber)
                    .Append(";")
                    .Append(labelledSequence.Label.TrimStart('>'))
                    .Append(";start=")
                    .Append(i + 1)
                    .Append(";end=")
                    .Append(i + fragmentLength)
                    .Append(";length=")
                    .Append(fragmentLength)
                    .Append(";source_length=")
                    .Append(originalLength)
                    .Append(";");
                string label = SequenceBuilder.ToString();
                SequenceBuilder.Clear();

                string fragment = sequence.Substring(i, fragmentLength);
                splitSequences.Add(label, fragment);
                fragmentNumber++;
            }
            return splitSequences;
        }
    }
}