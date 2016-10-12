using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Views;

namespace BioinformaticsSuite.Module.Models
{
    class MotifFinder
    {
        private SequenceType sequenceType;

        private readonly Regex dnaMotifValidator = new Regex("[^ACGTYRWSKMDVHBXN]", RegexOptions.Compiled);
        private readonly Regex mRnaMotifValidator = new Regex("[^ACGUYRWSKMDVHBXN]", RegexOptions.Compiled);
        private readonly Regex proteinMotifValidator = new Regex("[^ABCDEFGHIKLMNPQRSTVWXYZ]", RegexOptions.Compiled);

        private static readonly StringBuilder regexBuilder = new StringBuilder();

        public void FindMotif(string motif, List<LabelledSequence> labelledSequences)
        {
            sequenceType = labelledSequences.First().SequenceType;
            string motifPattern = BuildMotifPattern(motif);
            var motifRegex = new Regex(motifPattern);
            foreach (var labelledSequence in labelledSequences)
            {
                //  how to print a match
                //  index of matches must be linked in some way to the label
                //      1. Could be an instance variable of the labelledSequence object
                //      2. Could be a motif object.
                //      3. Potential for highlighting of text, how would this work?
                //      4. If this is a possinility, how would this gel with the line numbers and the insertion of line breaks to make the display look good.
                //      5. Note to self, have a look again at the automatic resizing of the window elements.
                //      6. Make a regex widget that lets you choose a letter eg 'Not P' drop down and a button to add to the regex string. Must also be a text box where the motif can be pasted in.
                /*
                Match match = motifRegex.Match(labelledSequence.Sequence);
                while (match.Success)
                {
                    string foundMotif = TrimOrf(match.Value);
                    if (foundOrf != string.Empty)
                    {
                        string orfLabel = BuildLabel(match);
                        openReadingFrames.Add(orfLabel, foundOrf);
                    }
                    match = orfMatcher.Match(proteinSequence, match.Index + 1);
                }
                */



                MatchCollection matchCollection = motifRegex.Matches(labelledSequence.Sequence);
            }
        }

        private string BuildMotifPattern(string motif)
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

        private bool IsValidDnaMotif(string motif)
        {

            return !dnaMotifValidator.IsMatch(motif);
        }

        private bool IsValidMRnaMotif(string motif)
        {
            return !mRnaMotifValidator.IsMatch(motif);
        }

        private bool IsValidProteinMotif(string motif)
        {
            return !proteinMotifValidator.IsMatch(motif);
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
                    default: throw new ArgumentException("Invalid Nucleotide in Motif");
                }
            }
            return regexBuilder.ToString();
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
            return regexBuilder.ToString();
        }
        private static string BuildProteinMotifPattern(string motif)
        {
            return motif;
            // UNDER CONSTRUCTION, may add custom regex UI builder thingy
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
            return regexBuilder.ToString();
            */
        }
    }

    public interface IMotifFinder
    {
        
    }
}
