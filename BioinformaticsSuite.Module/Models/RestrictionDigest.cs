using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;
using System.Text.RegularExpressions;

namespace BioinformaticsSuite.Module.Models
{
    
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
            enyzmes = RemoveEnzymeCutMarkers(enyzmes);
            foreach (var labelledSequence in labelledSequences)
            {
                FirstDigest(enyzmes.First(), labelledSequence);
                for (int i = 1; i < enyzmes.Count; i++)
                {
                    
                }
            }      
        }

        private DigestProduct FirstDigest(string enzyme, LabelledSequence labelledSequence)
        {
            var digestRegex = new Regex(enzyme, RegexOptions.Compiled);
            var rSiteIndices = new List<int[]>();
            var digestProducts = new List<string>();

            var matches = digestRegex.Matches(labelledSequence.Sequence);
            foreach (Match match in matches)
            {
                int startIndex = match.Index + 1;
                int endIndex = match.Index + match.Length;
                rSiteIndices.Add(new int[] { startIndex, endIndex });
                digestProducts.Add(match.Value);
            }
            var digestProduct = new DigestProduct(labelledSequence.Label, rSiteIndices, digestProducts);
            return digestProduct;
        }

        private DigestProduct SubsequentDigest(string enzyme, DigestProduct previousDigest)
        {
            var digestRegex = new Regex(enzyme, RegexOptions.Compiled);
            var rSiteIndices = new List<int[]>();

            foreach (string product in previousDigest.Products)
            {
                var matches = digestRegex.Matches(product);
            }
        }

        // Cut marker is '|' for sticky ends display purposes to the user.
        private static List<string> RemoveEnzymeCutMarkers(List<string> enzymes)
        {
            var replacer = new Regex("|");
            for (int i = 0; i < enzymes.Count; i++)
            {
                enzymes[i] = replacer.Replace(enzymes[i], "");
            }
            return enzymes;
        }




    }
    
}
