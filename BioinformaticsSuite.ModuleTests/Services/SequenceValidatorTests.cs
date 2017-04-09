using System.Collections.Generic;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Services
{
    [TestClass]
    public class SequenceValidatorTests
    {
        [TestMethod]
        public void ValidateDnaTest()
        {
            var sequenceValidator = SetupMock();

            var validTestDna = new Dictionary<string, string> { { ">test", "ACGT" } };
            var invalidTestDna = new Dictionary<string, string> { { ">test", "ACXT" } };
            var dnaErrorMessage = new ValidationErrorMessage(SequenceType.Dna, ">test", 3, "X", "Invalid DNA base detected");
            var expectedDnaType = dnaErrorMessage.ExpectedSequenceType;
            var expectedDnaLabel = dnaErrorMessage.Label;
            var expectedDnaIndex = dnaErrorMessage.ErrorIndex;
            var expectedDnaContent = dnaErrorMessage.ErrorContent;
            var expectedDnaDescription = dnaErrorMessage.ErrorDescription;

            Assert.IsTrue(sequenceValidator.TryValidateSequence(validTestDna, SequenceType.Dna));
            Assert.IsFalse(sequenceValidator.TryValidateSequence(invalidTestDna, SequenceType.Dna));

            Assert.AreEqual(expectedDnaType, sequenceValidator.ErrorMessage.ExpectedSequenceType);
            Assert.AreEqual(expectedDnaLabel, sequenceValidator.ErrorMessage.Label);
            Assert.AreEqual(expectedDnaIndex, sequenceValidator.ErrorMessage.ErrorIndex);
            Assert.AreEqual(expectedDnaContent, sequenceValidator.ErrorMessage.ErrorContent);
            Assert.AreEqual(expectedDnaDescription, sequenceValidator.ErrorMessage.ErrorDescription);
        }

        [TestMethod]
        public void ValidateRnaTest()
        {
            var sequenceValidator = SetupMock();

            var validTestRna = new Dictionary<string, string> { { ">test", "ACGU" } };
            var invalidTestRna = new Dictionary<string, string> { { ">test", "ACGY" } };

            var rnaErrorMessage = new ValidationErrorMessage(SequenceType.Rna, ">test", 4, "Y", "Invalid RNA base detected");
            var expectedRnaType = rnaErrorMessage.ExpectedSequenceType;
            var expectedRnaLabel = rnaErrorMessage.Label;
            var expectedRnaIndex = rnaErrorMessage.ErrorIndex;
            var expectedRnaContent = rnaErrorMessage.ErrorContent;
            var expectedRnaDescription = rnaErrorMessage.ErrorDescription;

            Assert.IsTrue(sequenceValidator.TryValidateSequence(validTestRna, SequenceType.Rna));
            Assert.IsFalse(sequenceValidator.TryValidateSequence(invalidTestRna, SequenceType.Rna));

            Assert.AreEqual(expectedRnaType, sequenceValidator.ErrorMessage.ExpectedSequenceType);
            Assert.AreEqual(expectedRnaLabel, sequenceValidator.ErrorMessage.Label);
            Assert.AreEqual(expectedRnaIndex, sequenceValidator.ErrorMessage.ErrorIndex);
            Assert.AreEqual(expectedRnaContent, sequenceValidator.ErrorMessage.ErrorContent);
            Assert.AreEqual(expectedRnaDescription, sequenceValidator.ErrorMessage.ErrorDescription);
        }

        [TestMethod]
        public void ValidateProteinTest()
        {
            var sequenceValidator = SetupMock();

            var validTestProtein = new Dictionary<string, string> { { ">test", "ACDEFGHIKLMNPQRSTVWY" } };
            var invalidTestProtein = new Dictionary<string, string> { { ">test", "ZCDEFGHIKLMNPQRSTVWY" } };

            var proteinErrorMessage = new ValidationErrorMessage(SequenceType.Protein, ">test", 1, "Z", "Invalid Amino acid detected");
            var expectedProteinType = proteinErrorMessage.ExpectedSequenceType;
            var expectedProteinLabel = proteinErrorMessage.Label;
            var expectedProteinIndex = proteinErrorMessage.ErrorIndex;
            var expectedProteinContent = proteinErrorMessage.ErrorContent;
            var expectedProteinDescription = proteinErrorMessage.ErrorDescription;

            Assert.IsTrue(sequenceValidator.TryValidateSequence(validTestProtein, SequenceType.Protein));
            Assert.IsFalse(sequenceValidator.TryValidateSequence(invalidTestProtein, SequenceType.Protein));

            Assert.AreEqual(expectedProteinType, sequenceValidator.ErrorMessage.ExpectedSequenceType);
            Assert.AreEqual(expectedProteinLabel, sequenceValidator.ErrorMessage.Label);
            Assert.AreEqual(expectedProteinIndex, sequenceValidator.ErrorMessage.ErrorIndex);
            Assert.AreEqual(expectedProteinContent, sequenceValidator.ErrorMessage.ErrorContent);
            Assert.AreEqual(expectedProteinDescription, sequenceValidator.ErrorMessage.ErrorDescription);
        }

        private static ISequenceValidator SetupMock()
        {
            return new SequenceValidator();
        }
    }
}