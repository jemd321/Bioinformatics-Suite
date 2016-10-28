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
        bool TryParseMotif(string motif, SequenceType motifSequenceType, out string parsedMotif);
        Dictionary<string, MatchCollection> FindMotif(string parsedMotif, List<LabelledSequence> labelledSequences);
        string InvalidMotifMessage { get; }
    }

    public class MotifFinder : IMotifFinder
    {
        private SequenceType sequenceType;
        private readonly Regex dnaMotifValidator = new Regex("[^ACGTYRWSKMDVHBXN]", RegexOptions.Compiled);
        private readonly Regex mRnaMotifValidator = new Regex("[^ACGUYRWSKMDVHBXN]", RegexOptions.Compiled);
        private readonly Regex proteinMotifValidator = new Regex("[^ABCDEFGHIKLMNPQRSTVWXYZ]", RegexOptions.Compiled);
        private static readonly StringBuilder regexBuilder = new StringBuilder();

        public string InvalidMotifMessage { get; private set; }

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
                        parsedMotif = BuildProteinMotifPattern(motif);
                        return true;

                    }
                    else return false;
                default:
                    throw new Exception("Sequence Type not recognised");
            }
        }

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

        public string BuildMotifPattern(string motif)
        {
            // Exceptions thrown here should be changed to return an invalid motif message to the user rather than throwing an E.
            switch (sequenceType)
            {
                case SequenceType.Dna:
                    if (IsValidDnaMotif(motif))
                    {
                        return BuildDnaMotifPattern(motif);
                    }
                    else throw new Exception("Invalid Dna Motif");
                case SequenceType.MRna:
                    if (IsValidMRnaMotif(motif))
                    {
                        return BuildMRnaMotifPattern(motif);
                    }
                    else throw new Exception("Invalid MRna Motif");
                case SequenceType.Protein:
                    if (IsValidProteinMotif(motif))
                    {
                        return BuildProteinMotifPattern(motif);
                    }
                    else throw new Exception("Invalid Protein Motif");
                default:
                    throw new Exception("Sequence Type not recognised, you really shouldnt be able to get here!");           
            }
        }

        private void BuildInvalidMotifErrorMessage(Match invalidMatch)
        {
            string invalidBase = invalidMatch.Value;
            var invalidCharIndex = invalidMatch.Index + 1;
            InvalidMotifMessage = "An invalid character (" + invalidBase + ") was found at position: " +
                                  invalidCharIndex;
        }

        private bool IsValidDnaMotif(string motif)
        {
            var match = dnaMotifValidator.Match(motif);           
            if (match.Success)
            {
                BuildInvalidMotifErrorMessage(match);
                return false;
            }
            else return true;
        }

        private bool IsValidMRnaMotif(string motif)
        {
            var match = mRnaMotifValidator.Match(motif);
            if (match.Success)
            {
                BuildInvalidMotifErrorMessage(match);
                return false;
            }
            else return true;
        }

        private bool IsValidProteinMotif(string motif)
        {
            var match = proteinMotifValidator.Match(motif);
            if (match.Success)
            {
                BuildInvalidMotifErrorMessage(match);
                return false;
            }
            else return true;
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
                    case 'Y':
                        regexBuilder.Append("[CT]");
                        break;
                    case 'R':
                        regexBuilder.Append("[AG]");
                        break;
                    case 'W':
                        regexBuilder.Append("[AT]");
                        break;
                    case 'S':
                        regexBuilder.Append("[GC]");
                        break;
                    case 'K':
                        regexBuilder.Append("[TG]");
                        break;
                    case 'M':
                        regexBuilder.Append("[CA]");
                        break;
                    case 'D':
                        regexBuilder.Append("[AGT]");
                        break;
                    case 'V':
                        regexBuilder.Append("[ACG]");
                        break;
                    case 'H':
                        regexBuilder.Append("[ACT]");
                        break;
                    case 'B':
                        regexBuilder.Append("[CGT]");
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
        private static string BuildProteinMotifPattern(string motif)
        {
            return motif;
            // UNDER CONSTRUCTION, may add custom regex UI builder
            /*
            foreach (char nucleotide in motif)
            {
                switch (nucleotide)
                {
                    case 'A':
                        regexBuilder.Append("A");
                        break;
                    case 'B':
                        regexBuilder.Append("C");
                        break;
                    case 'C':
                        regexBuilder.Append("G");
                        break;
                    case 'D':
                        regexBuilder.Append("T");
                        break;
                    case 'E':
                        regexBuilder.Append("[CT]");
                        break;
                    case 'F':
                        regexBuilder.Append("[AG]");
                        break;
                    case 'G':
                        regexBuilder.Append("[AT]");
                        break;
                    case 'H':
                        regexBuilder.Append("[GC]");
                        break;
                    case 'I':
                        regexBuilder.Append("[TG]");
                        break;
                    case 'K':
                        regexBuilder.Append("[CA]");
                        break;
                    case 'L':
                        regexBuilder.Append("[AGT]");
                        break;
                    case 'M':
                        regexBuilder.Append("[ACG]");
                        break;
                    case 'N':
                        regexBuilder.Append("[ACT]");
                        break;
                    case 'P':
                        regexBuilder.Append("[CGT]");
                        break;
                    case 'Q':
                        regexBuilder.Append("[ACGT]");
                        break;
                    case 'R':
                        regexBuilder.Append("[ACGT]");
                        break;
                    case 'S':
                        regexBuilder.Append("[ACGT]");
                        break;
                    case 'T':
                        regexBuilder.Append("[ACGT]");
                        break;
                    case 'V':
                        regexBuilder.Append("[ACGT]");
                        break;
                    case 'W':
                        regexBuilder.Append("[ACGT]");
                        break;
                    case 'X':
                        regexBuilder.Append("[ACGT]");
                        break;
                    case 'Y':
                        regexBuilder.Append("[ACGT]");
                        break;
                    case 'Z':
                        regexBuilder.Append("[ACGT]");
                        break;
                    default: throw new ArgumentException("Invalid Nucleotide in Motif");
                }
            }
            string resultMotif = regexBuilder.ToString();
            regexBuilder.Clear();
            return resultMotif;
            */
        }
    }
}
