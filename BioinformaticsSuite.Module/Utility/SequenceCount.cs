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


    }


}
