using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BioinformaticsSuite.Module.Enums;

namespace BioinformaticsSuite.Module.Services
{
    public interface IFastaParser
    {
        ParsingErrorMessage ErrorMessage { get; }
        Dictionary<string, string> ParsedSequences { get; }
        bool TryParseInput(string input);
        void ResetSequences();
    }

    public class FastaParser : IFastaParser
    {
        private readonly Regex _lineParser = new Regex("\n", RegexOptions.Compiled);
        private readonly StringBuilder _sequenceBuilder = new StringBuilder();
        private Dictionary<string, string> _parsedSequences = new Dictionary<string, string>();

        public ParsingErrorMessage ErrorMessage { get; private set; }
        public Dictionary<string, string> ParsedSequences => _parsedSequences;

        public bool TryParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = BuildErrorMessage(0, "No input sequences entered");
                return false;
            }

            var lines = _lineParser.Split(input).ToList();
            lines = RemoveEmptyLines(lines);
            lines = RemoveTerminators(lines);

            // Parser accepts either single or multiple labelled sequence in FASTA format 
            // (labels = single line with initial >), or a single unlabelled Sequence.
            bool isSuccess;
            bool containsLabels = CheckForSequenceLabels(lines);
            if (containsLabels) 
            {
                isSuccess = ParseLabelledSequences(lines);
                return isSuccess;
            }
            isSuccess = ParseUnlabelledSequence(lines);
            return isSuccess;
        }

        public void ResetSequences()
        {
            ErrorMessage = null;
            _parsedSequences = new Dictionary<string, string>();
        }

        // Parses only one sequence if it has no label
        private bool ParseUnlabelledSequence(List<string> inputLines)
        {
            try
            {
                // Concatenate lines into one sequence
                foreach (string line in inputLines)
                {
                    _sequenceBuilder.Append(Regex.Replace(line, "\\s", ""));
                }
                string parsedSequence = _sequenceBuilder.ToString().ToUpper();
                _sequenceBuilder.Clear();

                _parsedSequences.Add(">Unlabelled_Sequence", parsedSequence);
                return true;
            }
            catch (Exception)
            {
                ErrorMessage = BuildErrorMessage(1,
                    "An unspecified error occured while parsing this sequence");
                Debug.WriteLine("Single-Unlabelled sequence Input box text parsing failed without being able to identify the problem with the input");
                return false;
            }
        }

        // Parses single or multiple labelled sequences
        private bool ParseLabelledSequences(List<string> inputLines)
        {
            try
            {
                bool previousLineIsLabel;
                string label;
                var sequence = string.Empty;

                // Check for that first label has a sequence following
                if (inputLines.Count == 1)
                {
                    ErrorMessage = BuildErrorMessage(1, "All labels Must Have a sequence attached");
                    return false;
                }

                // Check that first label is not just '>'
                string firstLine = inputLines.First();
                if (firstLine.StartsWith(">")) // Ie. Is label
                {
                    if (firstLine.Length == 1)
                    {
                        ErrorMessage = BuildErrorMessage(1, "Labels must not consist solely of '>'");
                        return false;
                    }
                    label = firstLine;
                    previousLineIsLabel = true;
                }
                else
                {
                    ErrorMessage = BuildErrorMessage(1, "Labels must begin with a '>' character");
                    return false;
                }

                for (int i = 1; i < inputLines.Count; i++)
                {
                    string line = inputLines[i];
                    int lineNumber = i + 1;

                    string lineTag = IdentifyLineAsEitherSequenceOrLabel(line);
                    switch (lineTag)
                    {
                        case "label":
                            if (previousLineIsLabel)
                            {
                                ErrorMessage = BuildErrorMessage(lineNumber, "Each label must have a sequence attached");
                                return false;
                            }
                            if (line.Length == 1)
                            {
                                ErrorMessage = BuildErrorMessage(lineNumber, "Labels must not consist solely of '>'");
                                return false;
                            }
                            sequence = _sequenceBuilder.ToString();
                            if (sequence == string.Empty)
                            {
                                ErrorMessage = BuildErrorMessage(lineNumber, "Each label must have a sequence attached");
                                return false;
                            }
                            _sequenceBuilder.Clear();
                            if (LabelIsDuplicate(label))
                            {
                                ErrorMessage = BuildErrorMessage(lineNumber, "Duplicate sequence detected (" + label + ")");
                                return false;
                            }

                            _parsedSequences.Add(label, sequence);
                            label = line;
                            break;

                        case "sequence":
                            line = line.ToUpper();
                            _sequenceBuilder.Append(line);
                            previousLineIsLabel = false;
                            break;
                    }
                }
                sequence = _sequenceBuilder.ToString();

                // Process Last 
                if (LabelIsDuplicate(label))
                {
                    ErrorMessage = BuildErrorMessage(inputLines.Count, "Duplicate labelled sequence detected: (" + label + ")");
                }
                else if (sequence == string.Empty)
                {
                    ErrorMessage = BuildErrorMessage(inputLines.Count, "Each label must have a sequence attached");
                    return false;
                }

                // All parsed successfully
                _parsedSequences.Add(label, sequence);
                _sequenceBuilder.Clear();
                return true;
            }
            catch (Exception)
            {
                ErrorMessage = BuildErrorMessage(0, "An unknown error occured during sequence parsing");
                Debug.WriteLine("Mutli-sequence Input box text parsing failed without being able to identify the problem with the input");
                return false;
            }        
        }

        private static ParsingErrorMessage BuildErrorMessage(int lineNumber, string errorDescription)
        {
            return new ParsingErrorMessage(lineNumber, errorDescription);
        }

        private bool LabelIsDuplicate(string label)
        {
            return ParsedSequences.ContainsKey(label);
        }

        private static string IdentifyLineAsEitherSequenceOrLabel(string line)
        {
            return line.StartsWith(">") ? "label" : "sequence";
        }

        private static bool CheckForSequenceLabels(List<string> lines)
        {
            bool containsLabels = false;
            foreach (string line in lines)
            {
                if (line.StartsWith(">"))
                {
                    containsLabels = true;
                }
            }
            return containsLabels;
        }

        private static List<string> RemoveEmptyLines(List<string> lines)
        {
            lines.RemoveAll(string.IsNullOrWhiteSpace);
            return lines;
        }

        private static List<string> RemoveTerminators(List<string> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i] = Regex.Replace(lines[i], "\\s", "");
            }
            return lines;
        }
    }
}