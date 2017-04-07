using System.Text.RegularExpressions;
using BioinformaticsSuite.Module.Enums;
using Microsoft.Practices.ObjectBuilder2;

namespace BioinformaticsSuite.Module.Services
{
    public interface ISequenceValidator
    {
        ValidationErrorMessage ErrorMessage { get; }
        bool ValidateLabel(string label);
        bool ValidateSequence(string sequence, SequenceType sequenceType);
    }

    // Sequence Validator matches invalid sequence chars and logs the error content and location as properties.
    public class SequenceValidator : ISequenceValidator
    {
        private readonly Regex _dnaRegex = new Regex("[^ACGT]", RegexOptions.Compiled);
        private readonly Regex _rnaRegex = new Regex("[^ACGU]", RegexOptions.Compiled);
        private readonly Regex _proteinRegex = new Regex("[^ACDEFGHIKLMNQPRSTVWY*]", RegexOptions.Compiled);

        public ValidationErrorMessage ErrorMessage { get;  private set; }

        public bool ValidateLabel(string label)
        {
            return label != ">";
        }

        public bool ValidateSequence(string sequence, SequenceType sequenceType)
        {
            switch (sequenceType)
            {
                case SequenceType.Dna:
                    return IsValidDna(sequence);
                case SequenceType.Protein:
                    return IsValidProtein(sequence);
                case SequenceType.Rna:
                    return IsValidRna(sequence);
                default:
                    return false;
            }
        }

        private bool IsValidDna(string sequence)
        {
            var match = _dnaRegex.Match(sequence);
            if (!match.Success) return true;
            BuildErrorMessage(match, SequenceType.Dna);
            return false;
        }

        private bool IsValidRna(string sequence)
        {
            var match = _rnaRegex.Match(sequence);
            if (!match.Success) return true;
            BuildErrorMessage(match, SequenceType.Rna);
            return false;
        }

        private bool IsValidProtein(string sequence)
        {
            var match = _proteinRegex.Match(sequence);
            if (!match.Success) return true;
            BuildErrorMessage(match, SequenceType.Protein);
            return false;
        }

        private void BuildErrorMessage(Match match, SequenceType sequenceType)
        {
            int errorIndex = match.Index + 1;
            string errorContent = match.Value;

            string errorMessage;
            switch (sequenceType)
            {
                case SequenceType.Dna:
                    errorMessage = "Invalid DNA base detected";
                    break;
                case SequenceType.Rna:
                    errorMessage = "Invalid RNA base detected";
                    break;
                case SequenceType.Protein:
                    errorMessage = "Invalid Amino acid detected";
                    break;
                default:
                    errorMessage = "";
                    break;
            }
            ErrorMessage = new ValidationErrorMessage(sequenceType, errorIndex, errorContent, errorMessage);
        }
    }
}