using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Resources;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Views;

namespace BioinformaticsSuite.Module.Models
{
    public interface IMotifFinder
    {
        Dictionary<string, MatchCollection> FindMotif(string parsedMotif, List<LabelledSequence> labelledSequences);
        bool TryParseMotif(string motif, SequenceType motifSequenceType, out string parsedMotif);
        string InvalidMotifMessage { get; }
    }

    public class MotifFinder : IMotifFinder
    {
        private SequenceType sequenceType;
        private readonly Regex dnaMotifValidator = new Regex("[^ACGTYRWSKMDVHBXN]", RegexOptions.Compiled);
        private readonly Regex mRnaMotifValidator = new Regex("[^ACGUYRWSKMDVHBXN]", RegexOptions.Compiled);
        private readonly Regex proteinMotifValidator = new Regex("[^ACDEFGHIKLMNPQRSTVWY]", RegexOptions.Compiled);
        private static readonly StringBuilder regexBuilder = new StringBuilder();

        public string InvalidMotifMessage { get; private set; }

        // Searches all supplied sequences for the motif and foreach sequence returns a labelled collection of matches.
        public Dictionary<string, MatchCollection> FindMotif(string parsedMotif, List<LabelledSequence> labelledSequences)
        {
            var motifRegex = new Regex(parsedMotif);
            var labelledMatches = new Dictionary<string, MatchCollection>();
            foreach (var labelledSequence in labelledSequences)
            {
                var matchCollection = motifRegex.Matches(labelledSequence.Sequence);
                labelledMatches.Add(labelledSequence.Label, matchCollection);
            }
            return labelledMatches;
        }

        // Validates the user supplied motif then builds and passes out a regex string that will be used to search for the motif.
        public bool TryParseMotif(string motif, SequenceType motifSequenceType, out string parsedMotif)
        {
            parsedMotif = "";
            sequenceType = motifSequenceType;

            switch (sequenceType)
            {
                case SequenceType.Dna:
                    if (IsValidDnaMotif(motif))
                    {
                        parsedMotif = BuildDnaMotifPattern(motif);
                        return true;
                    }
                    else return false;
                case SequenceType.MRna:
                    if (IsValidMRnaMotif(motif))
                    {
                        parsedMotif = BuildMRnaMotifPattern(motif);
                        return true;
                    }
                    else return false;
                case SequenceType.Protein:
                    if (IsValidProteinMotif(motif))
                    {
                        parsedMotif = motif;
                        return true;

                    }
                    else return false;
                default:
                    throw new Exception("Sequence Type not recognised");
            }
        }

        private bool IsValidDnaMotif(string motif)
        {
            var match = dnaMotifValidator.Match(motif);
            if (!match.Success) return true;
            BuildInvalidMotifErrorMessage(match);
            return false;
        }

        private bool IsValidMRnaMotif(string motif)
        {
            var match = mRnaMotifValidator.Match(motif);
            if (!match.Success) return true;
            BuildInvalidMotifErrorMessage(match);
            return false;
        }

        private bool IsValidProteinMotif(string motif)
        {
            var match = proteinMotifValidator.Match(motif);
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
                        regexBuilder.Append("A");
                        break;
                    case 'C':
                        regexBuilder.Append("C");
                        break;
                    case 'G':
                        regexBuilder.Append("G");
                        break;
                    case 'T':
                        regexBuilder.Append("T");
                        break;
                    case 'R':
                        regexBuilder.Append("[AG]");
                        break;
                    case 'Y':
                        regexBuilder.Append("[CT]");
                        break;
                    case 'S':
                        regexBuilder.Append("[GC]");
                        break;
                    case 'W':
                        regexBuilder.Append("[AT]");
                        break;
                    case 'K':
                        regexBuilder.Append("[GT]");
                        break;
                    case 'M':
                        regexBuilder.Append("[AC]");
                        break;
                    case 'B':
                        regexBuilder.Append("[CGT]");
                        break;
                    case 'D':
                        regexBuilder.Append("[AGT]");
                        break;
                    case 'H':
                        regexBuilder.Append("[ACT]");
                        break;
                    case 'V':
                        regexBuilder.Append("[ACG]");
                        break;
                    case 'X':
                        regexBuilder.Append("[ACGT]");
                        break;
                    case 'N':
                        regexBuilder.Append("[ACGT]");
                        break;
                    default: throw new ArgumentException("Invalid Nucleotide in Motif - Parsing Method has missed it");
                }
            }
            string resultMotif = regexBuilder.ToString();
            regexBuilder.Clear();
            return resultMotif;
        }

        private static string BuildMRnaMotifPattern(string motif)
        {
            foreach (char nucleotide in motif)
            {
                switch (nucleotide)
                {
                    case 'A':
                        regexBuilder.Append("A");
                        break;
                    case 'C':
                        regexBuilder.Append("C");
                        break;
                    case 'G':
                        regexBuilder.Append("G");
                        break;
                    case 'U':
                        regexBuilder.Append("U");
                        break;
                    case 'Y':
                        regexBuilder.Append("[CU]");
                        break;
                    case 'R':
                        regexBuilder.Append("[AG]");
                        break;
                    case 'W':
                        regexBuilder.Append("[AU]");
                        break;
                    case 'S':
                        regexBuilder.Append("[GC]");
                        break;
                    case 'K':
                        regexBuilder.Append("[UG]");
                        break;
                    case 'M':
                        regexBuilder.Append("[CA]");
                        break;
                    case 'D':
                        regexBuilder.Append("[AGU]");
                        break;
                    case 'V':
                        regexBuilder.Append("[ACG]");
                        break;
                    case 'H':
                        regexBuilder.Append("[ACU]");
                        break;
                    case 'B':
                        regexBuilder.Append("[CGU]");
                        break;
                    case 'X':
                        regexBuilder.Append("[ACGU]");
                        break;
                    case 'N':
                        regexBuilder.Append("[ACGU]");
                        break;
                    default: throw new ArgumentException("Invalid Nucleotide in Motif");
                }
            }
            string resultMotif = regexBuilder.ToString();
            regexBuilder.Clear();
            return resultMotif;
        }
    }
}
