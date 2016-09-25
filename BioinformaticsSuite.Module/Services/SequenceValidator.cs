using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsSuite.Module.Models;
using Microsoft.Win32;

namespace BioinformaticsSuite.Module.Services
{
    public class SequenceValidator : ISequenceValidator
    {
        private readonly Regex dnaRegex = new Regex("[^ACTG]", RegexOptions.Compiled);
        private readonly Regex proteinRegex = new Regex("[^ACDEFGHIKLMNQPRSTVWY]", RegexOptions.Compiled);

        public int ErrorCharNumber { get; private set; }
        public string ErrorContent { get; private set; }

        public bool ValidateSequence(string sequence, SequenceType sequenceType)
        {
            switch (sequenceType)
            {
                case SequenceType.Dna:
                    return IsValidDna(sequence);
                case SequenceType.Protein:
                    return IsValidProtein(sequence);
                case SequenceType.MRna:
                    return IsValidMRna(sequence);
                default:
                    return false;
            }
        }

        private bool IsValidDna(string sequence)
        {
            Match match = dnaRegex.Match(sequence);
            if (match.Success)
            {               
                ErrorCharNumber = match.Index;
                ErrorContent = match.Value;
                return false;
            }
            return true;
        }

        private bool IsValidProtein(string sequence)
        {
            Match match = proteinRegex.Match(sequence);
            if (match.Success)
            {
                ErrorCharNumber = match.Index;
                ErrorContent = match.Value;
                return false;
            }
            return true;
        }

        private bool IsValidMRna(string sequence)
        {
            return true;
            //Todo
        }
    }

    public interface ISequenceValidator
    {
        int ErrorCharNumber { get; }
        string ErrorContent { get; }
        bool ValidateSequence(string sequence, SequenceType sequenceType);
    }
}
