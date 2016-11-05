using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsSuite.Module.Models
{
    public class DigestProduct
    {
        public DigestProduct(string label, List<int[]> rSitesIndices, List<string> products)
        {
            Label = label;
            RSiteIndices = rSitesIndices;
            Products = products;
        }

        public string Label { get; }
        public List<int[]> RSiteIndices { get; }
        public List<string> Products { get; }
    }
}
