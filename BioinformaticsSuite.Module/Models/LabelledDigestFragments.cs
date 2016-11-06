using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsSuite.Module.Models
{
    public class LabelledDigestFragments
    {
        public LabelledDigestFragments(string label, List<DigestFragment> digestFragments)
        {
            Label = label;
            DigestFramgments = digestFragments;
        }

        public string Label { get; }
        public List<DigestFragment> DigestFramgments { get; }
    }
}
