using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Rosalind_Locating_Restriction_Sites;

namespace Rosalind_Open_Reading_Frames
{
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
           // ReadBlah();
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

        private static void ReadBlah()
        {
            string path = @"resultRSites.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string wow = reader.ReadToEnd();
                wow = wow.TrimEnd('\n');
                wow = wow.TrimEnd('\r');
                //wow = wow.Replace("\r", "");            
                File.WriteAllText(@"woop.txt", wow);
            }

        
        }
    }
}
