using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Bioinformatics_Suite
{

    // For this file, eventually get rid of the Print methods, and split the write to file methods off into a custom dedicated class.
    // When hooked up to the UI, you should be able to specifiy which particular operation you want, eg get open reading frames.

    public class OpenReadingFrames
    {
        static void Main(string[] args)
        {
            Dna dna = new Dna(new FileInfo(@"rosalind_revp.txt"));
            //ReadingFrame readingFrame = new ReadingFrame(dna);
           // OpenReadingFrame openReadingFrame = new OpenReadingFrame(readingFrame);
            RestrictionSiteHelper r = new RestrictionSiteHelper(dna.Sequence, dna.ReversedComplement);
            List<string> blah = r.FindRestrictionSites();
           // PrintOrfs(openReadingFrame.Frames);
            PrintRestrictionSites(blah);
          //  WriteOrfsToTxtFile(openReadingFrame.Frames);
            WriteRestrictionSitesToTxxtFile(blah);

            
        }

        private static void PrintOrfs(List<string> orfs)
        {
            Console.WriteLine("The following ORFs were found, in no particular order:");
            orfs.ForEach(Console.WriteLine);
            Console.ReadKey();
        }

        private static void PrintRestrictionSites(List<string> restrictionSites)
        {
            Console.WriteLine("Restriction Sites were found at the following locations, followed by their lengths:");
            Console.WriteLine("\n\n");
            restrictionSites.ForEach(Console.WriteLine);
            Console.ReadKey();
        }

        private static void WriteOrfsToTxtFile(List<string> orfs)
        {
            File.WriteAllLines(@"resultOrfs.txt", orfs);
        }

        private static void WriteRestrictionSitesToTxxtFile(List<string> RSites)
        {
            File.WriteAllLines(@"resultRSites.txt", RSites);
        }
        
        }

}
