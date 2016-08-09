using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Bioinformatics_Suite
{

    // For this file, eventually get rid of the Print methods, and split the write to file methods off into a custom dedicated class.
    // This will become a factory class, as with Dad's main method call, instantiating a chain of classes and passing references to them.
    // When hooked up to the UI, you should be able to specifiy which particular operation you want, eg get open reading frames.

    public class OpenReadingFrames
    {
        static void Main(string[] args)
        {
            //Factory methods example

            //Dna dna = new Dna();
           // ReadingFrame readingFrame = new ReadingFrame(dna);
            //OpenReadingFrame openReadingFrame = new OpenReadingFrame(readingFrame);
            // end example

            //PrintOrfs(openReadingFrame.Frames);
            //WriteOrfsToTxtFile(openReadingFrame.Frames);           
        }

        private static void PrintOrfs(List<string> orfs)
        {
            Console.WriteLine("The following ORFs were found, in no particular order:");
            orfs.ForEach(Console.WriteLine);
            Console.ReadKey();
        }

        private static void WriteOrfsToTxtFile(List<string> orfs)
        {
            File.WriteAllLines(@"resultOrfs.txt", orfs);
        }
              
        }

}
