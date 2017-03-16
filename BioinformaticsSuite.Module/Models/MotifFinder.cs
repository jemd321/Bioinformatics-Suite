using System;
using System.Text;
using System.Text.RegularExpressions;
using BioinformaticsSuite.Module.Enums;

namespace BioinformaticsSuite.Module.Models
{
    public interface IMotifFinder
    {
        string InvalidMotifMessage { get; }
        MatchCollection FindMotif(string parsedMotif, LabelledSequence labelledSequence);
        bool TryParseMotif(string motif, SequenceType motifSequenceType, out string parsedMotif);
    }

    public class MotifFinder : IMotifFinder
    {
        private static readonly StringBuilder RegexBuilder = new StringBuilder();
        private readonly Regex _dnaMotifValidator = new Regex("[^ACGTYRWSKMDVHBXN]", RegexOptions.Compiled);
        private readonly Regex _proteinMotifValidator = new Regex("[^ACDEFGHIKLMNPQRSTVWY*]", RegexOptions.Compiled);
        private readonly Regex _rnaMotifValidator = new Regex("[^ACGUYRWSKMDVHBXN]", RegexOptions.Compiled);
        private SequenceType _sequenceType;

        public string InvalidMotifMessage { get; private set; }

        // Searches all supplied sequences for the motif and foreach sequence returns a labelled collection of matches.
        public MatchCollection FindMotif(string parsedMotif, LabelledSequence labelledSequence)
        {
            var motifRegex = new Regex(parsedMotif);
            var matches = motifRegex.Matches(labelledSequence.Sequence);
            return matches;
        }

        // Validates the user supplied motif then builds and passes out a regex string that will be used to search for the motif.
        public bool TryParseMotif(string motif, SequenceType motifSequenceType, out string parsedMotif)
        {
            parsedMotif = "";
            _sequenceType = motifSequenceType;

            switch (_sequenceType)
            {
                case SequenceType.Dna:
                    if (IsValidDnaMotif(motif))
                    {
                        parsedMotif = BuildDnaMotifPattern(motif);
                        return true;
                    }
                    return false;
                case SequenceType.Rna:
                    if (IsValidRnaMotif(motif))
                    {
                        parsedMotif = BuildRnaMotifPattern(motif);
                        return true;
                    }
                    return false;
                case SequenceType.Protein:
                    if (IsValidProteinMotif(motif))
                    {
                        parsedMotif = motif;
                        return true;
                    }
                    return false;
                default:
                    throw new Exception("Sequence Type not recognised");
            }
        }

        private bool IsValidDnaMotif(string motif)
        {
            var match = _dnaMotifValidator.Match(motif);
            if (!match.Success) return true;
            BuildInvalidMotifErrorMessage(match);
            return false;
        }

        private bool IsValidRnaMotif(string motif)
        {
            var match = _rnaMotifValidator.Match(motif);
            if (!match.Success) return true;
            BuildInvalidMotifErrorMessage(match);
            return false;
        }

        private bool IsValidProteinMotif(string motif)
        {
            var match = _proteinMotifValidator.Match(motif);
            if (!match.Success) return true;
            BuildInvalidMotifErrorMessage(match);
            return false;
        }

        private void BuildInvalidMotifErrorMessage(Match invalidMatch)
        {
            var invalidBase = invalidMatch.Value;
            var invalidCharIndex = invalidMatch.Index + 1;
            InvalidMotifMessage = "An invalid character (" + invalidBase + ") was found at position: " +
                                  invalidCharIndex;
        }

        private static string BuildDnaMotifPattern(string motif)
        {
            foreach (char nucleotide in motif)
            {
                switch (nucleotide)
                {
                    case 'A':
                        RegexBuilder.Append("A");
                        break;
                    case 'C':
                        RegexBuilder.Append("C");
                        break;
                    case 'G':
                        RegexBuilder.Append("G");
                        break;
                    case 'T':
                        RegexBuilder.Append("T");
                        break;
                    case 'R':
                        RegexBuilder.Append("[AG]");
                        break;
                    case 'Y':
                        RegexBuilder.Append("[CT]");
                        break;
                    case 'S':
                        RegexBuilder.Append("[GC]");
                        break;
                    case 'W':
                        RegexBuilder.Append("[AT]");
                        break;
                    case 'K':
                        RegexBuilder.Append("[GT]");
                        break;
                    case 'M':
                        RegexBuilder.Append("[AC]");
                        break;
                    case 'B':
                        RegexBuilder.Append("[CGT]");
                        break;
                    case 'D':
                        RegexBuilder.Append("[AGT]");
                        break;
                    case 'H':
                        RegexBuilder.Append("[ACT]");
                        break;
                    case 'V':
                        RegexBuilder.Append("[ACG]");
                        break;
                    case 'X':
                        RegexBuilder.Append("[ACGT]");
                        break;
                    case 'N':
                        RegexBuilder.Append("[ACGT]");
                        break;
                    default:
                        throw new ArgumentException("Invalid Nucleotide in Motif - Parsing Method has missed it");
                }
            }
            string resultMotif = RegexBuilder.ToString();
            RegexBuilder.Clear();
            return resultMotif;
        }

        private static string BuildRnaMotifPattern(string motif)
        {
            foreach (char nucleotide in motif)
            {
                switch (nucleotide)
                {
                    case 'A':
                        RegexBuilder.Append("A");
                        break;
                    case 'C':
                        RegexBuilder.Append("C");
                        break;
                    case 'G':
                        RegexBuilder.Append("G");
                        break;
                    case 'U':
                        RegexBuilder.Append("U");
                        break;
                    case 'Y':
                        RegexBuilder.Append("[CU]");
                        break;
                    case 'R':
                        RegexBuilder.Append("[AG]");
                        break;
                    case 'W':
                        RegexBuilder.Append("[AU]");
                        break;
                    case 'S':
                        RegexBuilder.Append("[GC]");
                        break;
                    case 'K':
                        RegexBuilder.Append("[UG]");
                        break;
                    case 'M':
                        RegexBuilder.Append("[CA]");
                        break;
                    case 'D':
                        RegexBuilder.Append("[AGU]");
                        break;
                    case 'V':
                        RegexBuilder.Append("[ACG]");
                        break;
                    case 'H':
                        RegexBuilder.Append("[ACU]");
                        break;
                    case 'B':
                        RegexBuilder.Append("[CGU]");
                        break;
                    case 'X':
                        RegexBuilder.Append("[ACGU]");
                        break;
                    case 'N':
                        RegexBuilder.Append("[ACGU]");
                        break;
                    default:
                        throw new ArgumentException("Invalid Nucleotide in Motif");
                }
            }
            string resultMotif = RegexBuilder.ToString();
            RegexBuilder.Clear();
            return resultMotif;
        }
    }
}