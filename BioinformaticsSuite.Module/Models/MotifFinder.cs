using System;
using System.CodeDom;
using System.Collections.Generic;
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

        private Regex motifRegex;

        public void FindMotif(string motif, List<LabelledSequence> labelledSequences)
        {
            var firstSequence = labelledSequences.First();
            sequenceType = firstSequence.SequenceType;
            ValidateMotif(motif);
            motifRegex = new Regex(motif);
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

        private void ValidateMotif(string motif)
        {
            switch (sequenceType)
            {
                case SequenceType.Dna:
                    ValidateDnaMotif(motif);
                    break;
                case SequenceType.MRna:
                    ValidateMRnaMotif(motif);
                    break;
                case SequenceType.Protein:
                    ValidateProteinMotif(motif);
                    break;
                default:
                    throw new Exception("oops");           
            }
        }

        private void ValidateDnaMotif(string motif)
        {
            
        }

        private void ValidateMRnaMotif(string motif)
        {
            
        }

        private void ValidateProteinMotif(string motif)
        {
            
        }


    }

    public interface IMotifFinder
    {
        
    }
}
