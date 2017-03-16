using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BioinformaticsSuite.Module.Models
{
    public interface IRestrictionDigest
    {
        List<LabelledDigestFragments> FindRestrictionDigestFragments(List<string> enzymes,
            List<LabelledSequence> labelledSequences);
    }

    public class RestrictionDigest : IRestrictionDigest
    {
        private readonly Regex _cutSiteRegex = new Regex(@"\|", RegexOptions.Compiled);

        public List<LabelledDigestFragments> FindRestrictionDigestFragments(List<string> enzymes,
            List<LabelledSequence> labelledSequences)
        {
            var digestResults = new List<LabelledDigestFragments>();
            foreach (var labelledSequence in labelledSequences)
            {
                // Converts the input sequence into a digestFragment object so one method can be used to process all subsequent enzyme digests.
                var inputSequence = new DigestFragment(enzymes.First(), 0, labelledSequence.Sequence);

                var digestFragments = new List<DigestFragment> {inputSequence};
                digestFragments = enzymes.Aggregate(digestFragments, (current, enzyme) => Digest(enzyme, current));
                digestResults.Add(new LabelledDigestFragments(labelledSequence.Label, digestFragments));
            }
            return digestResults;
        }

        private List<DigestFragment> Digest(string cutSiteEnzyme, List<DigestFragment> inputFragments)
        {
            int cutIndex = _cutSiteRegex.Match(cutSiteEnzyme).Index;
                // eg. AAG|CC gives cut index of 3, ie. the index of the char right of the cut marker once the marker is gone.
            string enzyme = _cutSiteRegex.Replace(cutSiteEnzyme, "");
            var digestRegex = new Regex(enzyme);

            var resultFragments = new List<DigestFragment>();
            foreach (var inputFragment in inputFragments)
            {
                string inputSequence = inputFragment.Fragment;
                int sequenceLength = inputSequence.Length;

                // Matches all restriction sites in the input sequence and creates a list of cut positions.
                const int sequenceStartIndex = 0;
                var cutPositions = new List<int> {sequenceStartIndex};
                cutPositions.AddRange(from Match match in digestRegex.Matches(inputSequence)
                    select match.Index + cutIndex);

                // if no matches found, just add the whole fragment to the resultList and move on to next fragment
                if (cutPositions.Count == 1)
                {
                    resultFragments.Add(new DigestFragment(inputFragment.Enzyme, inputFragment.CutPosition,
                        inputSequence));
                }
                else
                {
                    // Cuts sequence up into list of output fragments using cut positions
                    var initialFragment = inputSequence.Substring(cutPositions.First(), cutPositions[1]);
                    var outputFragments = new List<string> {initialFragment};
                    for (int i = 1; i < cutPositions.Count - 1; i++)
                    {
                        int cutPosition = cutPositions[i];
                        int fragmentLength = cutPositions[i + 1] - cutPosition;
                        outputFragments.Add(inputSequence.Substring(cutPosition, fragmentLength));
                    }
                    int finalCutPosition = cutPositions.Last();
                    outputFragments.Add(inputSequence.Substring(finalCutPosition, sequenceLength - finalCutPosition));

                    // Creates new DigestProduct object to hold each new fragment, and adds to the resultFragment list containing the fragments for the entire labelled input sequence
                    // Adjusts the cut position to match each fragment's relative position in the whole input sequence.
                    int previousDigestCutPosition = inputFragment.CutPosition;
                    int relativeCutPosition = previousDigestCutPosition + cutPositions[0];
                    resultFragments.Add(new DigestFragment(inputFragment.Enzyme, relativeCutPosition, outputFragments[0]));
                    for (int i = 1; i < outputFragments.Count; i++)
                    {
                        relativeCutPosition = previousDigestCutPosition + cutPositions[i];
                        resultFragments.Add(new DigestFragment(cutSiteEnzyme, relativeCutPosition, outputFragments[i]));
                    }
                }
            }
            return resultFragments;
        }
    }
}