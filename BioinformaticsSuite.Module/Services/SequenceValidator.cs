using System.Collections.Generic;
using System.Text.RegularExpressions;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using Microsoft.Practices.ObjectBuilder2;

namespace BioinformaticsSuite.Module.Services
{
    public interface ISequenceValidator
    {
        ValidationErrorMessage ErrorMessage { get; }
        bool TryValidateSequence(Dictionary<string, string> sequences, SequenceType sequenceType);
    }

    // Sequence Validator matches invalid sequence chars and logs the error content and location as properties.
    public class SequenceValidator : ISequenceValidator
    {
        private readonly Regex _dnaRegex = new Regex("[^ACGT]", RegexOptions.Compiled);
        private readonly Regex _rnaRegex = new Regex("[^ACGU]", RegexOptions.Compiled);
        private readonly Regex _proteinRegex = new Regex("[^ACDEFGHIKLMNQPRSTVWY*]", RegexOptions.Compiled);

        public ValidationErrorMessage ErrorMessage { get;  private set; }

        public bool TryValidateSequence(Dictionary<string, string> sequences, SequenceType sequenceType)
        {
            switch (sequenceType)
            {
                case SequenceType.Dna:
                    return IsValidDna(sequences);
                case SequenceType.Rna:
                    return IsValidRna(sequences);
                case SequenceType.Protein:
                    return IsValidProtein(sequences);
                default:
                    return false;
            }
        }

        private bool IsValidDna(Dictionary<string,string> sequences)
        {
            bool isValid = true;
            foreach (var labelledSequence in sequences)
            {

                var match = _dnaRegex.Match(labelledSequence.Value);
                if (match.Success) // if any non-DNA char is matched
                {
                    isValid = false;
                    BuildErrorMessage(SequenceType.Dna, labelledSequence.Key, match);
                }
            }
            return isValid;
        }

        private bool IsValidRna(Dictionary<string, string> sequences)
        {
            bool isValid = true;
            foreach (var labelledSequence in sequences)
            {
                var match = _rnaRegex.Match(labelledSequence.Value);
                if (match.Success) // if any non-DNA char is matched
                {
                    isValid = false;
                    BuildErrorMessage(SequenceType.Rna, labelledSequence.Key, match);
                }
            }
            return isValid;
        }

        private bool IsValidProtein(Dictionary<string, string> sequences)
        {
            bool isValid = true;
            foreach (var labelledSequence in sequences)
            {

                var match = _proteinRegex.Match(labelledSequence.Value);
                if (match.Success) // if any non-DNA char is matched
                {
                    isValid = false;
                    BuildErrorMessage(SequenceType.Protein, labelledSequence.Key, match);
                }
            }
            return isValid;
        }

        private void BuildErrorMessage(SequenceType sequenceType, string label, Match match)
        {
            int errorIndex = match.Index + 1;
            string errorContent = match.Value;

            string errorDescription;
            switch (sequenceType)
            {
                case SequenceType.Dna:
                    errorDescription = "Invalid DNA base detected";
                    break;
                case SequenceType.Rna:
                    errorDescription = "Invalid RNA base detected";
                    break;
                case SequenceType.Protein:
                    errorDescription = "Invalid Amino acid detected";
                    break;
                default:
                    errorDescription = "";
                    break;
            }
            ErrorMessage = new ValidationErrorMessage(sequenceType, label, errorIndex, errorContent, errorDescription);
        }
    }
}