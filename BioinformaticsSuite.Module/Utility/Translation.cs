using System;
using System.Collections.Generic;
using System.Text;

namespace BioinformaticsSuite.Module.Utility
{
    public static class Translation
    {
        private static readonly Dictionary<string, char> AminoAcidDictionary = new Dictionary<string, char>
        {
            {"GCU", 'A'},
            {"GCC", 'A'},
            {"GCA", 'A'},
            {"GCG", 'A'},
            {"CGU", 'R'},
            {"CGC", 'R'},
            {"CGA", 'R'},
            {"CGG", 'R'},
            {"AGA", 'R'},
            {"AGG", 'R'},
            {"AAU", 'N'},
            {"AAC", 'N'},
            {"GAU", 'D'},
            {"GAC", 'D'},
            {"UGU", 'C'},
            {"UGC", 'C'},
            {"CAA", 'Q'},
            {"CAG", 'Q'},
            {"GAA", 'E'},
            {"GAG", 'E'},
            {"GGU", 'G'},
            {"GGC", 'G'},
            {"GGA", 'G'},
            {"GGG", 'G'},
            {"CAU", 'H'},
            {"CAC", 'H'},
            {"AUU", 'I'},
            {"AUC", 'I'},
            {"AUA", 'I'},
            {"UUA", 'L'},
            {"UUG", 'L'},
            {"CUU", 'L'},
            {"CUC", 'L'},
            {"CUA", 'L'},
            {"CUG", 'L'},
            {"AAA", 'K'},
            {"AAG", 'K'},
            {"AUG", 'M'},
            {"UUU", 'F'},
            {"UUC", 'F'},
            {"CCU", 'P'},
            {"CCC", 'P'},
            {"CCA", 'P'},
            {"CCG", 'P'},
            {"UCU", 'S'},
            {"UCC", 'S'},
            {"UCA", 'S'},
            {"UCG", 'S'},
            {"AGU", 'S'},
            {"AGC", 'S'},
            {"ACU", 'T'},
            {"ACC", 'T'},
            {"ACA", 'T'},
            {"ACG", 'T'},
            {"UGG", 'W'},
            {"UAU", 'Y'},
            {"UAC", 'Y'},
            {"GUU", 'V'},
            {"GUC", 'V'},
            {"GUA", 'V'},
            {"GUG", 'V'},
            {"UAA", 'X'},
            {"UGA", 'X'},
            {"UAG", 'X'}
        };

        public static string TranslateDnaToProtein(string dnaSequence)
        {
            string rna = ConvertDnatoRna(dnaSequence);
            string protein = ConvertRnaToProtein(rna);
            return protein;
        }

        public static string TranscribeDnaToMRna(string dnaSequence)
        {
            string mRna = ConvertDnatoRna(dnaSequence);
            return mRna;
        }

        public static string TranslateMRnaToProtein(string mRNASequence)
        {
            string protein = ConvertRnaToProtein(mRNASequence);
            return protein;
        }

        private static string ConvertDnatoRna(string dna)
        {
            return dna.Replace('T', 'U');
        }

        private static string ConvertRnaToProtein(string rnaReadingFrame)
        {
            List<string> codons = SplitIntoCodons(rnaReadingFrame);
            return MatchAminoAcids(codons);
        }

        private static List<string> SplitIntoCodons(string rna)
        {
            List<string> codonList = new List<string>();

            int length = rna.Length;
            int remainder = length%3;
            int uselessBases = length - remainder;

            int index = 0;
            while (index < uselessBases)
            {
                codonList.Add(rna.Substring(index, 3));
                index += 3;
            }
            return codonList;
        }

        private static string MatchAminoAcids(List<string> codonList)
        {
            StringBuilder proteinBuilder = new StringBuilder();

            foreach (string codon in codonList)
            {
                if (AminoAcidDictionary.ContainsKey(codon))
                {
                    char aminoAcid;
                    AminoAcidDictionary.TryGetValue(codon, out aminoAcid);

                    proteinBuilder.Append(aminoAcid);
                }
                else
                {
                    //refactor this to not throw an excpetion when UI is implemented
                    throw new Exception("Invalid codon detected during codon translation");
                }
            }
            string protein = proteinBuilder.ToString();
            return protein;
        }
    }
}