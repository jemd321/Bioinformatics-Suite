using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BioinformaticsSuite.Module.Enums;

namespace BioinformaticsSuite.Module.Services
{
    public interface IFastaParser
    {
        ValidationErrorMessage ErrorMessage { get; }
        Dictionary<string, string> ParsedSequences { get; }
        bool TryParseInput(string input);
        void ResetSequences();
    }

    public class FastaParser : IFastaParser
    {
        private readonly Regex _lineParser = new Regex("\n", RegexOptions.Compiled);
        private readonly StringBuilder _sequenceBuilder = new StringBuilder();
        private Dictionary<string, string> _parsedSequences = new Dictionary<string, string>();

        public ValidationErrorMessage ErrorMessage { get; private set; }
        public Dictionary<string, string> ParsedSequences => _parsedSequences;

        public bool TryParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = BuildErrorMessage(0, null, "No input sequences entered");
                return false;
            }

            var lines = _lineParser.Split(input).ToList();
            lines = RemoveEmptyLines(lines);
            lines = RemoveTerminators(lines);

            bool isParsedSuccessfully;
            bool containsLabels = CheckForSequenceLabels(lines);
            if (containsLabels)
            {
                isParsedSuccessfully = ParseLabelledSequences(lines);
                return isParsedSuccessfully;
            }
            isParsedSuccessfully = ParseUnlabelledSequence(lines);
            return isParsedSuccessfully;
        }

        public void ResetSequences()
        {
            ErrorMessage = null;
            _parsedSequences = new Dictionary<string, string>();
        }

        // Parses only one sequence if it has no label
        private bool ParseUnlabelledSequence(List<string> inputLines)
        {
            foreach (string line in inputLines)
            {
                _sequenceBuilder.Append(Regex.Replace(line, "\\s", ""));
            }
            string parsedSequence = _sequenceBuilder.ToString();
            _sequenceBuilder.Clear();

            parsedSequence = parsedSequence.ToUpper();
            bool isValidSequence = _sequenceValidator.ValidateSequence(parsedSequence, _sequenceType);
            if (isValidSequence)
            {
                _parsedSequences.Add(">Unlabelled_Sequence", parsedSequence);
                return true;
            }
            const int errorLineNumber = 0;
            ErrorMessage = _sequenceValidator.ErrorMessage);
            return false;
        }

        // Parses single or multiple labelled sequences
        private bool ParseLabelledSequences(List<string> inputLines)
        {
            bool previousLineIsLabel = false;
            string label;
            string sequence = "";

            if (inputLines.Count == 1)
            {
                ErrorMessage = "All labels Must Have a sequence attached";
                return false;
            }

            string firstLine = inputLines[0];
            if (firstLine.StartsWith(">"))
            {
                if (!_sequenceValidator.ValidateLabel(firstLine))
                {
                    ErrorMessage = "Labels must not consist solely of '>'";
                    return false;
                }
                label = firstLine;
                previousLineIsLabel = true;
            }
            else
            {
                const int errorLineIndex = 0;
                const int errorCharIndex = 0;
                const string errorContent = "Labels must begin with a '>' character";
                ErrorMessage = BuildErrorMessage(errorLineIndex, errorCharIndex, errorContent);
                return false;
            }

            for (int i = 1; i < inputLines.Count; i++)
            {
                string line = inputLines[i];
                string lineTag = IdentifyLineAsEitherSequenceOrLabel(line);
                switch (lineTag)
                {
                    case "label":
                        if (previousLineIsLabel)
                        {
                            ErrorMessage = "Each label must have a sequence attached";
                            return false;
                        }
                        if (!_sequenceValidator.ValidateLabel(line))
                        {
                            ErrorMessage = "Labels must not consist solely of '>'";
                            return false;
                        }
                        sequence = _sequenceBuilder.ToString();
                        if (sequence == "")
                        {
                            ErrorMessage = "Each label must have a sequence attached";
                            break;
                        }
                        _sequenceBuilder.Clear();
                        if (LabelledSequenceIsDuplicate(label))
                        {
                            ErrorMessage = "Duplicate sequence detected (" + label + ")";
                        }
                        else
                        {
                            _parsedSequences.Add(label, sequence);
                            label = line;
                        }
                        break;

                    case "sequence":
                        line = line.ToUpper();
                        bool isValidSequence = _sequenceValidator.ValidateSequence(line, _sequenceType);
                        if (isValidSequence)
                        {
                            _sequenceBuilder.Append(line);
                            previousLineIsLabel = false;
                            break;
                        }
                        int errorLineNumber = i;
                        ErrorMessage = BuildErrorMessage(errorLineNumber, _sequenceValidator.ErrorIndex,
                            _sequenceValidator.ErrorContent);
                        return false;
                }
            }
            sequence = _sequenceBuilder.ToString();
            if (LabelledSequenceIsDuplicate(label))
            {
                ErrorMessage = "Duplicate labelled sequence detected: (" + label + ")";
            }
            else if (sequence == "")
            {
                ErrorMessage = "Each label must have a sequence attached";
                return false;
            }
            else
            {
                _parsedSequences.Add(label, sequence);
            }

            _sequenceBuilder.Clear();
            return true;
        }

        private ValidationErrorMessage BuildErrorMessage(int errorIndex, string errorContent, string errorMessage)
        {
            return new ValidationErrorMessage(_sequenceType, errorIndex, errorContent, errorMessage);
        }

        private bool LabelledSequenceIsDuplicate(string label)
        {
            bool isDuplicate = false;
            foreach (var labelledSequence in ParsedSequences)
            {
                string existinglabel = labelledSequence.Key;
                if (label == existinglabel)
                {
                    isDuplicate = true;
                }
            }
            return isDuplicate;
        }

        private string IdentifyLineAsEitherSequenceOrLabel(string line)
        {
            string lineTag = line.StartsWith(">") ? "label" : "sequence";
            return lineTag;
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