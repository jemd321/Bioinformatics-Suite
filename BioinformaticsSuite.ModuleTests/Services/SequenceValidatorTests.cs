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
        public void ValidateSequenceTest()
        {
            var sequenceValidator = SetupMock();

            var validTestDna = new Dictionary<string, string>{ {">test", "ACGT"} };
            var invalidTestDna = new Dictionary<string, string> { {">test", "ACXT" } };
            var expectedDnaErrorMessage = new ValidationErrorMessage(SequenceType.Dna, ">test", 3, "X", "Invalid DNA base detected");

            var validTestRna = new Dictionary<string, string> { {">test", "ACGU"} };
            var invalidTestRna = new Dictionary<string, string> { {">test", "ACGY"} };
            var expectedRnaErrorMessage = new ValidationErrorMessage(SequenceType.Rna, ">test", 4, "Y", "Invalid RNA base detected");


            var validTestProtein = new Dictionary<string, string> { {">test", "ACDEFGHIKLMNPQRSTVWY"} };
            var invalidTestProtein = new Dictionary<string, string> { {">test", "ZCDEFGHIKLMNPQRSTVWY"} };
            var expectedProteinErrorMessage = new ValidationErrorMessage(SequenceType.Protein, ">test", 1, "Z", "Invalid Amino acid detected");


            Assert.IsTrue(sequenceValidator.TryValidateSequence(validTestDna, SequenceType.Dna));
            Assert.IsFalse(sequenceValidator.TryValidateSequence(invalidTestDna, SequenceType.Dna));
            Assert.AreEqual(expectedDnaErrorMessage, sequenceValidator.ErrorMessage);

            Assert.IsTrue(sequenceValidator.TryValidateSequence(validTestRna, SequenceType.Rna));
            Assert.IsFalse(sequenceValidator.TryValidateSequence(invalidTestRna, SequenceType.Rna));
            Assert.AreEqual(expectedRnaErrorMessage, sequenceValidator.ErrorMessage);

            Assert.IsTrue(sequenceValidator.TryValidateSequence(validTestProtein, SequenceType.Protein));
            Assert.IsFalse(sequenceValidator.TryValidateSequence(invalidTestProtein, SequenceType.Protein));
            Assert.AreEqual(expectedProteinErrorMessage, sequenceValidator.ErrorMessage);
        }

        private static ISequenceValidator SetupMock()
        {
            return new SequenceValidator();
        }
    }
}