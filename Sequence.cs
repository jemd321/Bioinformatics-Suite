using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Bioinformatics_Suite
{
    public class Sequence

        //These Constructors must be modified to accept:
        // single strings, list of strings, text file of sequences, text file of FASTA format sequences.
    {
        public Sequence(string sequence)
        {
            if (sequence.StartsWith(">"))
            {
                string[] fasta = sequence.Split('\n');
                FastaSequences.Add(fasta[0], fasta[1]);
            }
            else
            {
                this.Sequence = sequence;
            }
        }

        public Sequence(FileInfo fileInfo)
        {
            string sequence = File.ReadAllText(fileInfo.FullName);
            this.Sequence = string.IsNullOrEmpty(sequence) ? string.Empty : Regex.Replace(sequence, @"\s", "");
        }

        public Sequence(List<string> SequenceList)
        {
            this.SequenceList = SequenceList;
        }

        public string Sequence;
        public List<string> SequenceList;
        public Dictionary<string, string> FastaSequences;

    }
}