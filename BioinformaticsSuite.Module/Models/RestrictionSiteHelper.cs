using System;
using System.Collections.Generic;
using System.Text;
using BioinformaticsSuite.Module.Utility;

namespace BioinformaticsSuite.Module.Models
{
    public class RestrictionSiteHelper
    {
        // Hook minRSLength and maxRSLength up to a GUI for the user to specificy in the big project later.
        // This can be made more efficient by not adding and checking odd numbered search frames to the arrays. It can also check from the middle of the search frame first rather than left to right.
        private int minRSLength = 4;
        private int maxRSLength = 12;
        private string forwardDna;
        private string reverseComplement;
        private List<string> RSiteList = new List<string>();

        private StringBuilder resultRSiteBuilder = new StringBuilder();

        public RestrictionSiteHelper(string forwardDna, string reverseComplement)
        {
            this.forwardDna = forwardDna;
            this.reverseComplement = reverseComplement;
        }

        public List<string> FindRestrictionSites()
        {
            int sequenceLength = forwardDna.Length;

            for (int dnaIndex = 0; dnaIndex < sequenceLength; dnaIndex++)
            {
                if (dnaIndex > sequenceLength - maxRSLength)
                {
                    maxRSLength--;
                    if (maxRSLength >= minRSLength)
                    {
                        CheckSubstringsForPalindromes(dnaIndex);
                    }
                }

                else
                {
                    CheckSubstringsForPalindromes(dnaIndex);
                }

            }
            return RSiteList;
        }

        private void CheckSubstringsForPalindromes(int dnaIndex)
        {
            int arraySize = (maxRSLength - minRSLength) + 1;

            string[] forwardSubstrings = GetPossibleRSitesArray(forwardDna.Substring(dnaIndex, maxRSLength), minRSLength, maxRSLength);
            string[] reverseSubstrings = new string[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                reverseSubstrings[i] = forwardSubstrings[i].Reverse().Complement();
            }
            foreach (string reverseSubstring in reverseSubstrings)
            {
                Console.WriteLine(reverseSubstring);
            }

            for (int rSiteArrayIndex = 0; rSiteArrayIndex < arraySize; rSiteArrayIndex++)
            {

                if (forwardSubstrings[rSiteArrayIndex].ValueEquals(reverseSubstrings[rSiteArrayIndex]) == true)
                {
                    int non0BasedDnaIndex = dnaIndex + 1;
                    string resultDnaIndex = non0BasedDnaIndex.ToString();
                    resultRSiteBuilder.Append(resultDnaIndex);
                    resultRSiteBuilder.Append(" ");
                    resultRSiteBuilder.Append(forwardSubstrings[rSiteArrayIndex].Length);

                    RSiteList.Add(resultRSiteBuilder.ToString());
                    resultRSiteBuilder.Clear();

                }              
            }
        }

        private static string[] GetPossibleRSitesArray(string dna, int minLength, int maxLength)
        {
            int arraySize = (maxLength - minLength) + 1;
            string[] possibleRSites = new string[arraySize];

            possibleRSites[0] = dna;
            for (int i = 1; i < arraySize; i++)
            {
                possibleRSites[i] = dna.Remove((maxLength - i), i);
            }
            return possibleRSites;
        }

    }
}