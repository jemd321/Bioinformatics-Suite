using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bioinformatics_Suite
{
    public class OpenReadingFrame
    {
        private readonly Dictionary<string, char> aminoAcidDictionary = new Dictionary<string, char>
            {
                {"GCU", 'A'},   {"GCC", 'A'},   {"GCA", 'A'},   {"GCG", 'A'},
                {"CGU", 'R'},   {"CGC", 'R'},   {"CGA", 'R'},   {"CGG", 'R'},   {"AGA", 'R'},   {"AGG", 'R'},
                {"AAU", 'N'},   {"AAC", 'N'},
                {"GAU", 'D'},   {"GAC", 'D'},
                {"UGU", 'C'},   {"UGC", 'C'},
                {"CAA", 'Q'},   {"CAG", 'Q'},
                {"GAA", 'E'},   {"GAG", 'E'},
                {"GGU", 'G'},   {"GGC", 'G'},   {"GGA", 'G'},   {"GGG", 'G'},
                {"CAU", 'H'},   {"CAC", 'H'},
                {"AUU", 'I'},   {"AUC", 'I'},   {"AUA", 'I'},
                {"UUA", 'L'},   {"UUG", 'L'},   {"CUU", 'L'},   {"CUC", 'L'},   {"CUA", 'L'},   {"CUG", 'L'},
                {"AAA", 'K'},   {"AAG", 'K'},
                {"AUG", 'M'},
                {"UUU", 'F'},   {"UUC", 'F'},
                {"CCU", 'P'},   {"CCC", 'P'},   {"CCA", 'P'},   {"CCG", 'P'},
                {"UCU", 'S'},   {"UCC", 'S'},   {"UCA", 'S'},   {"UCG", 'S'},   {"AGU", 'S'},   {"AGC", 'S'},
                {"ACU", 'T'},   {"ACC", 'T'},   {"ACA", 'T'},   {"ACG", 'T'},
                {"UGG", 'W'},
                {"UAU", 'Y'},   {"UAC", 'Y'},
                {"GUU", 'V'},   {"GUC", 'V'},   {"GUA", 'V'},   {"GUG", 'V'},
                {"UAA", 'X'},   {"UGA", 'X'},   {"UAG", 'X'},
            };


        public List<string> Frames { get; }

        public OpenReadingFrame(ReadingFrame readingFrame)
        {
            // I wrote this out and then Resharper suggested the more consise form below - can't say I can write in Linq straight away yet!

            //List<string> proteinReadingFrames = new List<string>();
            //foreach (string dnaReadingFrame in readingFrame.Frames)
            //{
            //    string rnaReadingFrame = ConvertDnatoRna(dnaReadingFrame);
            //    string proteinReadingFrame = ConvertRnaToProtein(rnaReadingFrame);
            //    proteinReadingFrames.Add(proteinReadingFrame);
            //}


            List<string> proteinReadingFrames = readingFrame.Frames.Select(ConvertDnatoRna).Select(ConvertRnaToProtein).ToList();

            this.Frames = FindOpenReadingFrames(proteinReadingFrames);
        }

        private static string ConvertDnatoRna(string dna)
        {
            return dna.Replace('T', 'U');
        }

        private string ConvertRnaToProtein(string rnaReadingFrame)
        {
            List<string> codons = SplitIntoCodons(rnaReadingFrame);
            return MatchAminoAcids(codons);
        }

        private static List<string> SplitIntoCodons(string rna)
        {
            List<string> codonList = new List<string>();

            int index = 0;
            while (index < rna.Length)
            {
                codonList.Add(rna.Substring(index, 3));
                index += 3;
            }
            return codonList;
        }

        private string MatchAminoAcids(List<string> codonList)
        {
            StringBuilder proteinBuilder = new StringBuilder();

            foreach (string codon in codonList)
            {
                if (aminoAcidDictionary.ContainsKey(codon))
                {
                    char aminoAcid;
                    aminoAcidDictionary.TryGetValue(codon, out aminoAcid);

                    proteinBuilder.Append(aminoAcid);
                }
                else
                {
                    throw new Exception("Invalid codon detected during codon translation");
                }
            }
            string proteinOrf = proteinBuilder.ToString();
            return proteinOrf;
        }

        private static List<string> FindOpenReadingFrames(List<string> readingFrames)
        {
            List<string> openReadingFrames = new List<string>();
            Regex orfMatcher = new Regex("M[^X]*X");

            foreach (string readingFrame in readingFrames)
            {
                Match match = orfMatcher.Match(readingFrame);
                while (match.Success)
                {
                    string foundOrf = TrimOrf(match.Value);
                    if (foundOrf != string.Empty && !openReadingFrames.Contains(foundOrf))
                    {
                        openReadingFrames.Add(foundOrf);
                    }
                    match = match.NextMatch();
                }
            }
            return openReadingFrames;
        }

        private static string TrimOrf(string untrimmedOrf)
        {
            return untrimmedOrf.TrimEnd('X');
        }
    }
}
