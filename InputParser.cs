using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace Bioinformatics_Suite
{
    public class InputParser
    {
        public InputParser(string sequence)
        {
            this.InputSequence = sequence;       
        }

        public InputParser(FileInfo fileInfo)
        {
            string sequence = File.ReadAllText(fileInfo.FullName);
            this.InputSequence = sequence;
        }

        public string InputSequence { get; }
        public string ParsedSequence => ParseSequence(this.InputSequence);
        public Dictionary<string, string> ParsedFasta => ParseFasta(this.InputSequence);

        private void ParseInput(string sequence)
        {
            if (sequence.StartsWith(">"))
            {
                ParseFasta(sequence);
            }
            else
            {
                ParseSequence(sequence);
            }
        }

        private string ParseSequence(string sequence)
        {
            return sequence = Regex.Replace(sequence, "\\s", "");
        }

        private Dictionary<string, string> ParseFasta(string sequence)
        {
            Regex fastaParser = new Regex("\n");
            string[] lines = fastaParser.Split(sequence);
            Dictionary<string, string> fastaDictionary = new Dictionary<string, string>();

            StringBuilder sequenceBuilder = new StringBuilder();

            string key = "";
            string value = "";
            var loopStart = true;

            foreach (string line in lines)
            {


                if (loopStart)
                {
                    if (line.StartsWith(">"))
                    {
                        key = Regex.Replace(line, "\\s", "");
                        loopStart = false;
                    }
                    else
                    {
                        // refactor me later to a UI message informing the user it's the wrong format instead of throwing an e.
                        throw new Exception("Input sequences do not conform to FASTA file format!");
                    }
                }
                else if (line.StartsWith(">"))
                {
                    value = sequenceBuilder.ToString();
                    sequenceBuilder.Clear();

                    key = Regex.Replace(line, "\\s", "");
                    fastaDictionary.Add(key, value);
                }
                else
                {
                    sequenceBuilder.Append(Regex.Replace(line, "\\s", ""));
                }
            }
            value = sequenceBuilder.ToString();
            fastaDictionary.Add(key, value);
            return fastaDictionary;
        }
    }
}