using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;

namespace BioinformaticsSuite.Module.Services
{
    public class SequenceParser : ISequenceParser
    {
        private SequenceType sequenceType;
        private Dictionary<string, string> parsedSequences = new Dictionary<string, string>();
        private readonly StringBuilder sequenceBuilder =  new StringBuilder();
        private readonly StringBuilder errorMessageBuilder = new StringBuilder();
        private readonly ISequenceValidator sequenceValidator;
        private readonly Regex lineParser = new Regex("\n", RegexOptions.Compiled);

        public SequenceParser(ISequenceValidator sequenceValidator)
        {
            this.sequenceValidator = sequenceValidator;
        }

        public string ErrorMessage { get; private set; }
        public Dictionary<string, string> ParsedSequences => parsedSequences;

        public bool TryParseInput(string input, SequenceType sequenceType)
        {
            this.sequenceType = sequenceType;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "No sequences entered";
                return false;
            }
          
            List<string> lines = lineParser.Split(input).ToList();
            lines = RemoveEmptyLines(lines);
            lines = RemoveTerminators(lines);

            bool isParsedSuccessfully;
            bool containsLabels = CheckForSequenceLabels(lines);
            if (containsLabels)
            {
                isParsedSuccessfully = ParseLabelledSequences(lines);
                return isParsedSuccessfully;
            }
            else
            {
                isParsedSuccessfully = ParseUnlabelledSequence(lines);
                return isParsedSuccessfully;
            }
        }

        // Parses only one sequence if it has no label
        private bool ParseUnlabelledSequence(List<string> inputLines)
        {
            foreach(string line in inputLines)
            {
                sequenceBuilder.Append(Regex.Replace(line, "\\s", ""));
            }
            string parsedSequence = sequenceBuilder.ToString();
            sequenceBuilder.Clear();

            parsedSequence = parsedSequence.ToUpper();
            bool isValidSequence = sequenceValidator.ValidateSequence(parsedSequence, sequenceType);
            if (isValidSequence)
            {
                parsedSequences.Add(">Unlabelled_Sequence", parsedSequence);
                return true;
            }
            else
            {
                const int errorLineNumber = 0;
                ErrorMessage = BuildErrorMessage(errorLineNumber, sequenceValidator.ErrorCharNumber, sequenceValidator.ErrorContent);
                return false;
            }
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
                        else
                        {
                            sequence = sequenceBuilder.ToString();
                            if (sequence == "")
                            {
                                ErrorMessage = "Each label must have a sequence attached";
                                break;
                            }
                            sequenceBuilder.Clear();
                            if (LabelledSequenceIsDuplicate(label))
                            {
                                ErrorMessage = "Duplicate sequence detected (" + label + ")";
                            }
                            else
                            {
                                parsedSequences.Add(label, sequence);
                                label = line;
                            }
                            break;
                        }

                        case "sequence":
                        line = line.ToUpper();
                        bool isValidSequence = sequenceValidator.ValidateSequence(line, sequenceType);
                        if (isValidSequence)
                        {
                            sequenceBuilder.Append(line);
                            previousLineIsLabel = false;
                            break;
                        }
                        else
                        {
                            int errorLineNumber = i;
                            ErrorMessage = BuildErrorMessage(errorLineNumber, sequenceValidator.ErrorCharNumber, sequenceValidator.ErrorContent);
                            return false;
                        }                            
                }
            }
            sequence = sequenceBuilder.ToString();
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
                parsedSequences.Add(label, sequence);
            }

            sequenceBuilder.Clear();
            return true;          
        }

        private string BuildErrorMessage(int errorLineNumber, int errorCharNumber, string errorContent)
        {
            errorMessageBuilder.Append("Invalid input was detected on line: ");
            errorMessageBuilder.Append(errorLineNumber + 1);
            errorMessageBuilder.Append(", at character: ");            
            errorMessageBuilder.Append(errorCharNumber + 1);
            errorMessageBuilder.Append(". An invalid character (");
            errorMessageBuilder.Append(errorContent);
            errorMessageBuilder.Append(") was found.");
            string errorMessage = errorMessageBuilder.ToString();
            errorMessageBuilder.Clear();
            return errorMessage;
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
            string lineTag;
            if (line.StartsWith(">"))
            {
                lineTag = "label";
            }
            else lineTag = "sequence";
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

        public void ResetSequences()
        {
            ErrorMessage = "";
            parsedSequences = new Dictionary<string, string>();
        }
    }

    public interface ISequenceParser
    {
        bool TryParseInput(string sequence, SequenceType sequenceType);
        string ErrorMessage { get; }
        Dictionary<string, string> ParsedSequences { get; }
        void ResetSequences();
    }
}