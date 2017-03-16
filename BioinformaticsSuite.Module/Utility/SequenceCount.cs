using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsSuite.Module.Utility
{
    public static class SequenceCount
    {
        public static int[] CountDnaBases(this string sequence)
        {
            int aCount = sequence.Count(n => n == 'A');
            int cCount = sequence.Count(n => n == 'C');
            int gCount = sequence.Count(n => n == 'G');
            int tCount = sequence.Count(n => n == 'T');

            int[] countArray = new[] {aCount, cCount, gCount, tCount};
            return countArray;
        }

        public static int[] CountRnaBases(this string sequence)
        {
            int aCount = sequence.Count(n => n == 'A');
            int cCount = sequence.Count(n => n == 'C');
            int gCount = sequence.Count(n => n == 'G');
            int uCount = sequence.Count(n => n == 'U');

            int[] countArray = new[] { aCount, cCount, gCount, uCount };
            return countArray;
        }

        public static Dictionary<char, int> CountAminoAcids(this string protein)
        {
            int a = protein.Count(x => x == 'A');
            int c = protein.Count(x => x == 'C');
            int d = protein.Count(x => x == 'D');
            int e = protein.Count(x => x == 'E');
            int f = protein.Count(x => x == 'F');
            int g = protein.Count(x => x == 'G');
            int h = protein.Count(x => x == 'H');
            int i = protein.Count(x => x == 'I');
            int k = protein.Count(x => x == 'K');
            int l = protein.Count(x => x == 'L');
            int m = protein.Count(x => x == 'M');
            int n = protein.Count(x => x == 'N');
            int p = protein.Count(x => x == 'P');
            int q = protein.Count(x => x == 'Q');
            int r = protein.Count(x => x == 'R');
            int s = protein.Count(x => x == 'S');
            int t = protein.Count(x => x == 'T');
            int v = protein.Count(x => x == 'V');
            int w = protein.Count(x => x == 'W');
            int y = protein.Count(x => x == 'Y');
            int stop = protein.Count(x => x == '*');

            var count = new Dictionary<char, int>()
            {
                {'A', a},
                {'C', c},
                {'D', d},
                {'E', e},
                {'F', f},
                {'G', g},
                {'H', h},
                {'I', i},
                {'K', k},
                {'L', l},
                {'M', m},
                {'N', n},
                {'P', p},
                {'Q', q},
                {'R', r},
                {'S', s},
                {'T', t},
                {'V', v},
                {'W', w},
                {'Y', y},
                {'*', stop }
            };
            return count;
        }


    }


}
