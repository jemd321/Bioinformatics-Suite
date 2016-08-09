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
            this.FastaString = sequence;
            ParseInput(this.FastaString);            
        }

        public InputParser(FileInfo fileInfo)
        {
            this.FastaString = File.ReadAllText(fileInfo.FullName);
            ParseInput(this.FastaString);
        }

        public string FastaString { get; set; }

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
            return sequence = Regex.Replace(sequence, @"\s", "");
        }

        private Dictionary<string, string> ParseFasta(string sequence)
        {
            Regex fastaParser = new Regex("\n");
            string[] lines = fastaParser.Split(sequence);
            Dictionary<string, string> fastaDictionary = new Dictionary<string, string>();

            StringBuilder sequenceBuilder = new StringBuilder();

            string key;
            string value;
            var loopStart = true;

            foreach (string line in lines)
            {
                if (loopStart)
                {
                    if (line.StartsWith(">"))
                    {
                        key = line;
                        loopStart = false;
                    }
                    else
                    {
                        // refactor me later to a message informing the user it's the wrong format instead of throwing an e.
                        // I am typing this to sound busy like I am programming something, which  frankl;y sinnple is'nt true. So there, show that up your ass and smopke it lo..for the nth time , 
                        throw new Exception("Input sequences do not conform to FASTA file format!");
                    }
                }
                else if (line.StartsWith(">"))
                {
                    value = sequenceBuilder.ToString();
                    sequenceBuilder.Clear();

                    key = line;
                    fastaDictionary.Add(key, value);
                }
                else
                {
                    sequenceBuilder.Append(line);
                }

            }
            return fastaDictionary;
        }
    }
}