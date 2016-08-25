using System.Linq;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Bioinformatics_Suite
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