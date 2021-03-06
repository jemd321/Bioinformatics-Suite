﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Models
{
    [TestClass]
    public class MotifFinderTests
    {
        [TestMethod]
        public void TryParseMotifTest()
        {
            var motifFinder = CreateTestInstance();

            const string testDnaMotif = "ACGTRYSWKMBDHVNX";
            const string testRnaMotif = "ACGURYSWKMBDHVNX";
            const string testProteinMotif = "ACDEFGHIKLMNPQRSTVW";

            const string expectedDnaMotif = "ACGT[AG][CT][GC][AT][GT][AC][CGT][AGT][ACT][ACG][ACGT][ACGT]";
            const string expectedRnaMotif = "ACGU[AG][CU][GC][AU][UG][CA][CGU][AGU][ACU][ACG][ACGU][ACGU]";
            const string expectedProteinMotif = "ACDEFGHIKLMNPQRSTVW";

            string actualDnaMotif;
            string actualRnaMotif;
            string actualProteinMotif;

            const string invalidDnaMotif = "ACZT";
            const string invalidRnaMotif = "AZGU";
            const string invalidProteinMotif = "ACDEFGHIJKLM";

            // Assert is Valid Motif
            Assert.IsTrue(motifFinder.TryParseMotif(testDnaMotif, SequenceType.Dna, out actualDnaMotif));
            Assert.IsTrue(motifFinder.TryParseMotif(testRnaMotif, SequenceType.Rna, out actualRnaMotif));
            Assert.IsTrue(motifFinder.TryParseMotif(testProteinMotif, SequenceType.Protein, out actualProteinMotif));

            // Assert has motif been correctly converted to a regex pattern
            Assert.AreEqual(expectedDnaMotif, actualDnaMotif);
            Assert.AreEqual(expectedRnaMotif, actualRnaMotif);
            Assert.AreEqual(expectedProteinMotif, actualProteinMotif);

            // Assert that an invalid motif is correctly identified
            Assert.IsFalse(motifFinder.TryParseMotif(invalidDnaMotif, SequenceType.Dna, out actualDnaMotif));
            Assert.IsFalse(motifFinder.TryParseMotif(invalidRnaMotif, SequenceType.Rna, out actualRnaMotif));
            Assert.IsFalse(motifFinder.TryParseMotif(invalidProteinMotif, SequenceType.Protein, out actualProteinMotif));
        }

        [TestMethod]
        public void FindMotifTest()
        {
            var motifFinder = CreateTestInstance();

            var testDnaSequence = new Dna("test2", "ACGTACGATCGTTGAGACGTACGTACGATCGTTGAG");
            var testRnaSequence = new Rna("test2", "ACGUACGAUCGUUGAGACGUACGUACGAUCGUUGAG");
            var testProteinSequence = new Protein("test2", "ABCDEFGHIKLMNPQRSTVWYABCDABCDEFGHIKLMNPQRSTVWY");

            const string parsedDnaMotif = "ACGT[AG][CT][GC][AT][TG][CA][CGT][AGT][ACT][ACG][ACGT][ACGT]";
            const string parsedRnaMotif = "ACGU[AG][CU][GC][AU][UG][CA][CGU][AGU][ACU][ACG][ACGU][ACGU]";
            const string parsedProteinMotif = "ABCDEFGHIKLMNPQRSTVWY";

            var actualDnaMotifs = motifFinder.FindMotif(parsedDnaMotif, testDnaSequence);
            var actualRnaMotifs = motifFinder.FindMotif(parsedRnaMotif, testRnaSequence);
            var actualProteinMotifs = motifFinder.FindMotif(parsedProteinMotif, testProteinSequence);

            var expectedDnaIndices = new List<int> {1, 16, 21, 36};
            var expectedRnaIndices = new List<int> {1, 16, 21, 36};
            var expectedProteinIndices = new List<int> {1, 21, 26, 46};

            var actualDnaIndices = ParseMatchCollections(actualDnaMotifs);
            var actualRnaIndices = ParseMatchCollections(actualRnaMotifs);
            var actualProteinIndicies = ParseMatchCollections(actualProteinMotifs);

            // Assert that the indices for each motif search are correct
            CollectionAssert.AreEquivalent(expectedDnaIndices, actualDnaIndices);
            CollectionAssert.AreEquivalent(expectedRnaIndices, actualRnaIndices);
            CollectionAssert.AreEquivalent(expectedProteinIndices, actualProteinIndicies);
        }

        private static IMotifFinder CreateTestInstance()
        {
            IMotifFinder motifFinder = new MotifFinder();
            return motifFinder;
        }

        private static List<int> ParseMatchCollections(MatchCollection matches)
        {
            var actualIndices = new List<int>();
            foreach (Match match in matches)
            {
                var startIndex = match.Index + 1;
                var endIndex = startIndex + match.Length - 1;
                actualIndices.Add(startIndex);
                actualIndices.Add(endIndex);
            }
            return actualIndices;
        }
    }
}