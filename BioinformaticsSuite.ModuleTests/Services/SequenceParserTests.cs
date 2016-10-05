using System.Collections.Generic;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Services
{
    [TestClass()]
    public class SequenceParserTests
    {   
        private const SequenceType SequenceType = Module.Enums.SequenceType.Dna;

        public ISequenceParser InstantiateParser()
        {
            ISequenceParser sequenceParser = new SequenceParser(new SequenceValidator());
            return sequenceParser;
        }

        [TestMethod()]
        public void ParseLabelAndSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = ">HelloWorld\nACTAACTA";
            var expectedSequences = new Dictionary<string, string> {{">HelloWorld", "ACTAACTA"}};

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);
            var actualSequences = sequenceParser.ParsedSequences;

            Assert.IsTrue(isSuccess);
            CollectionAssert.AreEquivalent(expectedSequences, actualSequences);
        }

        [TestMethod()]
        public void ParseLabelAndMultiLineSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = ">HelloWorld\nACTAACTA\nACTAACTA";
            var expectedSequences = new Dictionary<string, string> { { ">HelloWorld", "ACTAACTAACTAACTA" } };

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);
            var actualSequences = sequenceParser.ParsedSequences;

            Assert.IsTrue(isSuccess);
            CollectionAssert.AreEquivalent(expectedSequences, actualSequences);
        }


        [TestMethod()]
        public void ParseEmptyBox()
        {
            var sequenceParser = InstantiateParser();
            var testCase = "";
            var expectedError = "No sequences entered";

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);
            var actualError = sequenceParser.ErrorMessage;

            Assert.IsFalse(isSuccess);
            Assert.AreEqual(expectedError, actualError);
        }

        [TestMethod()]
        public void ParseOnlyLabel()
        {
            var sequenceParser = InstantiateParser();
            var testCase = ">HelloWorld";

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);

            Assert.IsFalse(isSuccess);
        }


        [TestMethod()]
        public void ParseOnlySequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = "ACTA";
            var expectedSequences = new Dictionary<string, string> { { ">Unlabelled_Sequence", "ACTA" } };

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);
            var actualSequences = sequenceParser.ParsedSequences;

            Assert.IsTrue(isSuccess);
            CollectionAssert.AreEquivalent(expectedSequences, actualSequences);
        }

        [TestMethod()]
        public void ParseOnlyMultiLineSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = "ACTA\nACTA";
            var expectedSequences = new Dictionary<string, string> { { ">Unlabelled_Sequence", "ACTAACTA" } };

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);
            var actualSequences = sequenceParser.ParsedSequences;

            Assert.IsTrue(isSuccess);
            CollectionAssert.AreEquivalent(expectedSequences, actualSequences);
        }

        [TestMethod()]
        public void ParseLabelWithNoSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = ">HelloWorld\n\n";

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod()]
        public void ParseLabelWithNoSequenceFollowedByLabelAndSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = ">HelloWorld\n\n>HelloOtherWorld\nACTA";
           
            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod()]
        public void ParseLabelWithSequenceFollowedLabelWithNoSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = ">HelloWorld\nACTA\n>HelloOtherWorld";

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod()]
        public void ParseSequenceFollowedByLabel()
        {
            var sequenceParser = InstantiateParser();
            var testCase = "ACTA\n>HelloWorld";

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod()]
        public void ParseSequenceFollowedByLabelAndSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = "ACTA\n>HelloWorld\nACTA";

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod()]
        public void ParseInvalidSingleSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = "ACTAP";

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod()]
        public void ParseLabelAndInvalidSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = ">HelloWorld\nACTAP";

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod()]
        public void ParseLabelAndInvalidMutliLineSequence()
        {
            var sequenceParser = InstantiateParser();
            var testCase = ">HelloWorld\nACTA\nACTAP";

            bool isSuccess = sequenceParser.TryParseInput(testCase, SequenceType);

            Assert.IsFalse(isSuccess);
        }
    }
}