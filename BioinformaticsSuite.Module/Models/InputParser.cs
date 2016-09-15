using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BioinformaticsSuite.Module.Models
{
    public class InputParser
    {
        public InputParser(string sequence)
        {
            InputSequence = sequence;  
            ParseInput(sequence);     
        }

        public InputParser(FileInfo fileInfo)
        {
            string sequence = File.ReadAllText(fileInfo.FullName);
            InputSequence = sequence;
            ParseInput(sequence);
        }

        public string InputSequence { get; }
        public Dictionary<string, string> ParsedSequences = new Dictionary<string, string>();

        private void ParseInput(string sequence)
        {
            if (sequence.StartsWith(">"))
            {
                ParseFasta(sequence);
            }
            else
            {
                ParseUnlabelledSequence(sequence);
            }
        }

        private void ParseUnlabelledSequence(string sequence)
        {
            Dictionary<string, string> unlabelledSequence = new Dictionary<string, string>();
            string parsedSequence = sequence = Regex.Replace(sequence, "\\s", "");
            ParsedSequences.Add(">No Label" , parsedSequence);
        }

        private void ParseFasta(string sequence)
        {
            Regex fastaParser = new Regex("\n");
            string[] lines = fastaParser.Split(sequence);

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
                    ParsedSequences.Add(key, value);
                }
                else
                {
                    sequenceBuilder.Append(Regex.Replace(line, "\\s", ""));
                }
            }
            value = sequenceBuilder.ToString();
            ParsedSequences.Add(key, value);
        }
    }

    public interface IInputParser
    {
       
    }
}