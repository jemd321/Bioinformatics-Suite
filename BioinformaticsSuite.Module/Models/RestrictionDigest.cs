using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;

namespace BioinformaticsSuite.Module.Models
{
    public interface IRestrictionDigest
    {
        
    }

    public class RestrictionDigest
    {

        // Fields
        private readonly IMotifFinder motifFinder;

        // Restriction Enzymes




            // Constructors
        public RestrictionDigest(IMotifFinder motifFinder)
        {
            this.motifFinder = motifFinder;
            if(motifFinder == null) throw new ArgumentNullException(nameof(motifFinder));
        }

        //Properties

        public void FindRestrictionDigestProducts(RestrictionEnzymes[] enyzmes, List<Dna> labelledSequences)
        {
            /*
            foreach (var labelledSequence in labelledSequences)
            {
                foreach (var enyzme in enyzmes)
                {
                    var label = labelledSequence.Label;
                    var sequence = labelledSequence.Sequence;

                    int[] digestProductIndices = FindRSiteIndices(enyzme, sequence);
                    foreach(var digestProduct in )
                    DigestProduct digestProduct = new DigestProduct(label, digestProductIndices);
                    var labelledDigestProducts = new Dictionary<string, DigestProduct>();
                    labelledDigestProducts.
                }
            }
            */
        }

        private void FindRSiteIndices(string enyzme, string sequence)
        {
            // finds the indices of the passed in sequence

            // USE struct or something for enzymes instead of enum? Ideally I would like
            // the User input enzyme name to be passed to the method in an array, then, with the
            // corresponding sequence implicitly attached to the name of the enzyme.
        }

        private void Digest(string enzyme, string sequence)
        {
            // returns substrings based on the indices found by the motif finder.
        }




    }
}
