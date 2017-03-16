using System.Collections.Generic;
using System.IO;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;

namespace BioinformaticsSuite.Module.Services
{
    public class SequenceFactory : ISequenceFactory
    {
        public List<LabelledSequence> CreateLabelledSequences(Dictionary<string, string> sequences,
            SequenceType sequenceType)
        {
            List<LabelledSequence> labelledSequences = new List<LabelledSequence>();

            switch (sequenceType)
            {
                case SequenceType.Dna:
                    labelledSequences = CreateDnaInstances(sequences);
                    break;
                case SequenceType.Protein:
                    labelledSequences = CreateProteinInstances(sequences);
                    break;
                case SequenceType.Rna:
                    labelledSequences = CreateRnaInstances(sequences);
                    break;
            }
            return labelledSequences;
        }

        //this doesn't really belong in this class. Move elsewhere.
        public string ImportFromTxtFile(FileInfo fileInfo)
        {
            string unparsedSequence = fileInfo.FullName;
            return unparsedSequence;
        }

        private List<LabelledSequence> CreateDnaInstances(Dictionary<string, string> sequences)
        {
            var labelledSequences = new List<LabelledSequence>();

            foreach (KeyValuePair<string, string> sequencePair in sequences)
            {
                string label = sequencePair.Key;
                string sequence = sequencePair.Value;
                labelledSequences.Add(new Dna(label, sequence));
            }
            return labelledSequences;
        }

        private List<LabelledSequence> CreateProteinInstances(Dictionary<string, string> sequences)
        {
            var labelledSequences = new List<LabelledSequence>();

            foreach (KeyValuePair<string, string> sequencePair in sequences)
            {
                string label = sequencePair.Key;
                string sequence = sequencePair.Value;
                labelledSequences.Add(new Protein(label, sequence));
            }
            return labelledSequences;
        }

        private List<LabelledSequence> CreateRnaInstances(Dictionary<string, string> sequences)
        {
            var labelledSequences = new List<LabelledSequence>();

            foreach (KeyValuePair<string, string> sequencePair in sequences)
            {
                string label = sequencePair.Key;
                string sequence = sequencePair.Value;
                labelledSequences.Add(new Rna(label, sequence));
            }
            return labelledSequences;
        }
    }

    public interface ISequenceFactory
    {
        List<LabelledSequence> CreateLabelledSequences(Dictionary<string, string> sequences, SequenceType sequenceType);
        string ImportFromTxtFile(FileInfo fileInfo);
    }
}