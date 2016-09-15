using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioinformaticsSuite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Models;

namespace BioinformaticsSuite.Tests
{
    [TestClass()]
    public class InputParserTests
    {
        [TestMethod()]
        public void SingleSequenceConstructorTest()
        {
            InputParser parser = new InputParser("ACTG\n");
            var expected = "ACTG\n";
            var actual = parser.InputSequence;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FileInfoConstructorTest()
        {
            InputParser parser = new InputParser(new FileInfo(@"testInputSequences.txt"));
            var expected = ">fastaheader\r\nACTG\r\nTGCA\r\n";
            var actual = parser.InputSequence;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ParseSingleSequenceTest()
        {
            InputParser parser = new InputParser("ACTG\n");
            var expected = "ACTG";

            var sequence = parser.ParsedSequences;
            string actual;
            sequence.TryGetValue(">No Label", out actual);
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ParseFastaTest()
        {
            InputParser parser = new InputParser(new FileInfo(@"testInputSequences.txt"));
            var expected = new Dictionary<string, string>()
            {
                {">fastaheader", "ACTGTGCA"}
            };

            var actual = parser.ParsedSequences;

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}