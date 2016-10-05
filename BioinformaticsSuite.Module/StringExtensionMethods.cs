using System.Linq;
using System.Text;

namespace BioinformaticsSuite.Module
{
    public static class StringReverser
    {
        public static string Reverse(this string input)
        {
            return new string(input.ToCharArray().Reverse().ToArray());          
        }

        public static string Complement(this string input)
        {
            StringBuilder complement = new StringBuilder();
            foreach (char sequenceBase in input)
            {
                if (sequenceBase == 'A') { complement.Append('T'); };
                if (sequenceBase == 'T') { complement.Append('A'); };
                if (sequenceBase == 'C') { complement.Append('G'); };
                if (sequenceBase == 'G') { complement.Append('C'); };
            }
            return complement.ToString();
        }

        // custom 'Equals' method to compare elements in a collection by value, since the bog standard collection .Equals only checks the collection references for equality.
        public static bool ValueEquals(this string forwardElement, string reverseElement)
        {
            if (forwardElement.Equals(reverseElement))
            {
                return true;
            }
            else return false;
        }
    }
}