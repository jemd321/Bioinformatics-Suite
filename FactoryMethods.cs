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

        //Note to self - clean up all the crappy comments at some point.

    public static class FactoryMethods
    {
        //These properties will be bound to an XAML element, eg a text box in which the user supplies the input.
        private static FileInfo filePath = new FileInfo(@"test.txt");
        public static string Sequence { get; private set; }

        static void Main(string[] args)
        {
            //just for testing purposes until UI set up to supply sequences
              
        }

        private static void Factory()
        {

            // Dont create an variable and assign an object to it, instead foreach pair use a list.add methods to create a new object defined by its unique fields
            // eg, List.add(new Dna(">AJIDK", "ACGTTGCA");
            // SO, need to modify Dna to accept a fasta label and a sequence rather than an input parser object.
            // then use the factory methods to create a single new input parser object that instantiates as many DNA objects as are needed to store the data.

        }

        private static void FindReadingFrames()
        {
            
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
