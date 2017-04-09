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
            var sequenceValidator = CreateTestInstance();

            const string validTestDna = "ACGT";
            const string invalidTestDna = "ACXT";
            const int expectedDnaErrorIndex = 3;
            const string expectedDnaErrorContent = "X";

            const string validTestRna = "ACGU";
            const string invalidTestRna = "ACGY";
            const int expectedRnaErrorIndex = 4;
            const string expectedRnaErrorContent = "Y";

            const string validTestProtein = "ACDEFGHIKLMNPQRSTVWY";
            const string invalidTestProtein = "ZCDEFGHIKLMNPQRSTVWY";
            const int expectedProteinErrorIndex = 1;
            const string expectedProteinErrorContent = "Z";

            Assert.IsTrue(sequenceValidator.TryValidateSequence(validTestDna, SequenceType.Dna));
            Assert.IsFalse(sequenceValidator.TryValidateSequence(invalidTestDna, SequenceType.Dna));
            Assert.AreEqual(expectedDnaErrorIndex, sequenceValidator.ErrorIndex);
            Assert.AreEqual(expectedDnaErrorContent, sequenceValidator.ErrorContent);

            Assert.IsTrue(sequenceValidator.TryValidateSequence(validTestRna, SequenceType.Rna));
            Assert.IsFalse(sequenceValidator.TryValidateSequence(invalidTestRna, SequenceType.Rna));
            Assert.AreEqual(expectedRnaErrorIndex, sequenceValidator.ErrorIndex);
            Assert.AreEqual(expectedRnaErrorContent, sequenceValidator.ErrorContent);

            Assert.IsTrue(sequenceValidator.TryValidateSequence(validTestProtein, SequenceType.Protein));
            Assert.IsFalse(sequenceValidator.TryValidateSequence(invalidTestProtein, SequenceType.Protein));
            Assert.AreEqual(expectedProteinErrorIndex, sequenceValidator.ErrorIndex);
            Assert.AreEqual(expectedProteinErrorContent, sequenceValidator.ErrorContent);
        }

        private static ISequenceValidator CreateTestInstance()
        {
            ISequenceValidator sequenceValidator = new SequenceValidator();
            return sequenceValidator;
        }
    }
}