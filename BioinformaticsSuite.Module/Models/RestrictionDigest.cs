using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;

namespace BioinformaticsSuite.Module.Models
{
    /*
    public interface IRestrictionDigest
    {
        
    }

    public class RestrictionDigest
    {
        private readonly IMotifFinder motifFinder;

        public RestrictionDigest(IMotifFinder motifFinder)
        {
            this.motifFinder = motifFinder;
            if(motifFinder == null) throw new ArgumentNullException(nameof(motifFinder));
        }

        public void FindRestrictionDigestProducts(List<string> enyzmes, List<Dna> labelledSequences)
        {
            
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
        }

        private List<int[]> FindRSiteIndices(string enyzme, string sequence)
        {
            motifFinder.FindMotif(enzyme, )
        }

        private void Digest(string enzyme, string sequence)
        {
            // returns substrings based on the indices found by the motif finder.
        }




    }
    */
}
