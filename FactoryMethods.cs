using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace Bioinformatics_Suite
{

    // For this file, eventually get rid of the Print methods, and split the write to file methods off into a custom dedicated class.
    // This will become a factory class, instantiating a chain of classes and passing references to them.
    // When hooked up to the UI, you should be able to specifiy which particular operation you want, eg get open reading frames.

        //Note to self - clean up all the crappy comments at some point.

    public static class FactoryMethods
    {
        //These properties will be bound to an XAML element, eg a text box in which the user supplies the input.
        private static FileInfo filePath = new FileInfo(@"test.txt");
        public static string Sequence { get; private set; }

        static void Main(string[] args)
        {
            //just for testing purposes until UI set up to supply sequences
            string hello = "world";

        }

        private static void Factory()
        {

        }

        private static void Import()
        {
            ImportSequence();
            //DetermineSequenceType();
            CreateDnaInstances();
        }

        private static void ImportFactory()
        {
            ImportSequence();
        }

        private static List<OpenReadingFrame> FindOpenReadingFrames(List<Protein> proteins)
        {
            var openReadingFrames = new List<OpenReadingFrame>();

            foreach (Protein protein in proteins)
            {
                openReadingFrames.Add(new OpenReadingFrame(protein));
            }

            return openReadingFrames;
        }


        private static List<ReadingFrame> FindReadingFrames(List<Dna> dnaList)
        {
            var readingFrames = new List<ReadingFrame>();

            foreach (Dna dna in dnaList)
            {
                readingFrames.Add(new ReadingFrame(dna));
            }
            return readingFrames;
        }

        private static Dictionary<string, string> ImportSequence()
        {
            InputParser parser = new InputParser(filePath);
            return parser.ParsedSequences;
        }

        private static void CreateDnaInstances()
        {
            var parsedSequences = ImportSequence();
            var sequenceList = new List<Dna>();

            foreach (var sequencePair in parsedSequences)
            {
                sequenceList.Add(new Dna(sequencePair.Key, sequencePair.Value));
            }
            
        }

        private static void FindORFs()
        {
            
        }

        private static void TranslateDna()
        {
            
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
