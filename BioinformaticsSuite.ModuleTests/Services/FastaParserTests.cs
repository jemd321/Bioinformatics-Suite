using System.Collections.Generic;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Services
{
    [TestClass]
    public class FastaParserTests
    {
        private const SequenceType SequenceType = Module.Enums.SequenceType.Dna;



        [TestMethod]
        public void ParseLabelAndSequence()
        {
            var sequenceParser = SetupMock();
            var testCase = ">HelloWorld\nACTAACTA";
            var expectedSequences = new Dictionary<string, string> {{">HelloWorld", "ACTAACTA"}};

            bool isSuccess = sequenceParser.TryParseInput(testCase);
            var actualSequences = sequenceParser.ParsedSequences;

            Assert.IsTrue(isSuccess);
            CollectionAssert.AreEquivalent(expectedSequences, actualSequences);
        }

        [TestMethod]
        public void ParseLabelAndMultiLineSequence()
        {
            var sequenceParser = SetupMock();
            var testCase = ">HelloWorld\nACTAACTA\nACTAACTA";
            var expectedSequences = new Dictionary<string, string> {{">HelloWorld", "ACTAACTAACTAACTA"}};

            bool isSuccess = sequenceParser.TryParseInput(testCase);
            var actualSequences = sequenceParser.ParsedSequences;

            Assert.IsTrue(isSuccess);
            CollectionAssert.AreEquivalent(expectedSequences, actualSequences);
        }


        [TestMethod]
        public void ParseEmptyBox()
        {
            var sequenceParser = SetupMock();
            var testCase = "";
            var expectedError = new ParsingErrorMessage(0, "No input sequences entered");

            bool isSuccess = sequenceParser.TryParseInput(testCase);
            var actualError = sequenceParser.ErrorMessage;

            Assert.IsFalse(isSuccess);
            Assert.AreEqual(expectedError.LineNumber, actualError.LineNumber);
            Assert.AreEqual(expectedError.ErrorDescription, actualError.ErrorDescription);
        }

        [TestMethod]
        public void ParseOnlyLabel()
        {
            var sequenceParser = SetupMock();
            var testCase = ">HelloWorld";

            bool isSuccess = sequenceParser.TryParseInput(testCase);

            Assert.IsFalse(isSuccess);
        }


        [TestMethod]
        public void ParseOnlySequence()
        {
            var sequenceParser = SetupMock();
            var testCase = "ACTA";
            var expectedSequences = new Dictionary<string, string> {{">Unlabelled_Sequence", "ACTA"}};

            bool isSuccess = sequenceParser.TryParseInput(testCase);
            var actualSequences = sequenceParser.ParsedSequences;

            Assert.IsTrue(isSuccess);
            CollectionAssert.AreEquivalent(expectedSequences, actualSequences);
        }

        [TestMethod]
        public void ParseOnlyMultiLineSequence()
        {
            var sequenceParser = SetupMock();
            var testCase = "ACTA\nACTA";
            var expectedSequences = new Dictionary<string, string> {{">Unlabelled_Sequence", "ACTAACTA"}};

            bool isSuccess = sequenceParser.TryParseInput(testCase);
            var actualSequences = sequenceParser.ParsedSequences;

            Assert.IsTrue(isSuccess);
            CollectionAssert.AreEquivalent(expectedSequences, actualSequences);
        }

        [TestMethod]
        public void ParseLabelWithNoSequence()
        {
            var sequenceParser = SetupMock();
            var testCase = ">HelloWorld\n\n";

            bool isSuccess = sequenceParser.TryParseInput(testCase);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void ParseLabelWithNoSequenceFollowedByLabelAndSequence()
        {
            var sequenceParser = SetupMock();
            var testCase = ">HelloWorld\n\n>HelloOtherWorld\nACTA";

            bool isSuccess = sequenceParser.TryParseInput(testCase);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void ParseLabelWithSequenceFollowedLabelWithNoSequence()
        {
            var sequenceParser = SetupMock();
            var testCase = ">HelloWorld\nACTA\n>HelloOtherWorld";

            bool isSuccess = sequenceParser.TryParseInput(testCase);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void ParseSequenceFollowedByLabel()
        {
            var sequenceParser = SetupMock();
            var testCase = "ACTA\n>HelloWorld";

            bool isSuccess = sequenceParser.TryParseInput(testCase);

            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void ParseSequenceFollowedByLabelAndSequence()
        {
            var sequenceParser = SetupMock();
            var testCase = "ACTA\n>HelloWorld\nACTA";

            bool isSuccess = sequenceParser.TryParseInput(testCase);

            Assert.IsFalse(isSuccess);
        }

        public IFastaParser SetupMock()
        {
            return new FastaParser();
        }
    }
}