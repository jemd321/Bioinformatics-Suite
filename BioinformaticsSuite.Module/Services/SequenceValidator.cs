using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using Microsoft.Win32;

namespace BioinformaticsSuite.Module.Services
{
    public interface ISequenceValidator
    {
        int ErrorIndex { get; }
        string ErrorContent { get; }
        bool ValidateLabel(string label);
        bool ValidateSequence(string sequence, SequenceType sequenceType);
    }

    // Sequence Validator matches invalid sequence chars and logs the error content and location as properties.
    public class SequenceValidator : ISequenceValidator
    {
        private readonly Regex _dnaRegex = new Regex("[^ACGT]", RegexOptions.Compiled);
        private readonly Regex _rnaRegex = new Regex("[^ACGU]", RegexOptions.Compiled);
        private readonly Regex _proteinRegex = new Regex("[^ACDEFGHIKLMNQPRSTVWY]", RegexOptions.Compiled);

        public int ErrorIndex { get; private set; }
        public string ErrorContent { get; private set; }

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
            LogErrorInfo(match);
            return false;
        }

        private bool IsValidRna(string sequence)
        {
            var match = _rnaRegex.Match(sequence);
            if (!match.Success) return true;
            LogErrorInfo(match);
            return false;
        }

        private bool IsValidProtein(string sequence)
        {
            var match = _proteinRegex.Match(sequence);
            if (!match.Success) return true;
            LogErrorInfo(match);
            return false;
        }

        private void LogErrorInfo(Match match)
        {
            ErrorIndex = match.Index + 1;
            ErrorContent = match.Value;
        }
    }
}
