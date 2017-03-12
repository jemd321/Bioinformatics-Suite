using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioinformaticsSuite.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsSuite.Module.Models.Tests
{
    [TestClass()]
    public class FastaManipulatorTests
    {
        [TestMethod()]
        public void CombineFastaTest()
        {
            var fastaManipulator = SetupMock();
            var testSequences = new List<LabelledSequence>() { new Dna("test1", "ACGTCCGGTGCACCGGCCGGACGT"), new Dna("test2", "CCGGTGCACCGGCCGG") };
            var expectedLabel = ">40 base sequence from 2 sequences";
            var expectedSequence = "ACGTCCGGTGCACCGGCCGGACGTCCGGTGCACCGGCCGG";

            Dictionary<string, string> combinedSequence = fastaManipulator.CombineFasta(testSequences);

            Assert.AreEqual(1, combinedSequence.Count);
            Assert.AreEqual(expectedLabel, combinedSequence.First().Key);
            Assert.AreEqual(expectedSequence, combinedSequence.First().Value);
        }

        [TestMethod()]
        public void SplitFastaTest()
        {
            var fastaManipulator = SetupMock();
            var testSequence = new Dna("test", "ACGTCCGGTGCACCGGCCGGACGT");
            var testFragmentLength = 12;
            var expectedSplitSequences = new Dictionary<string, string>()
            {
                {">fragment_1;test;start=1;end=12;length=12;source_length=24;", "ACGTCCGGTGCA" },
                {">fragment_2;test;start=13;end=24;length=12;source_length=24;", "CCGGCCGGACGT" }
            };

            var actualSplitSequences = fastaManipulator.SplitFasta(testSequence, testFragmentLength);

            Assert.AreEqual(2, actualSplitSequences.Count);
            CollectionAssert.AreEquivalent(expectedSplitSequences, actualSplitSequences);
        }

        private static IFastaManipulator SetupMock()
        {
            return new FastaManipulator();
        }
    }
}