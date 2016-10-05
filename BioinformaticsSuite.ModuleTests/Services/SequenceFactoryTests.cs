using System.Collections.Generic;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Services
{
    [TestClass()]
    public class SequenceFactoryTests
    {
        [TestMethod()]
        public void CreateDnaInstancesTest()
        {
            var sequenceFactory = CreateSequenceFactory();
            var testCase = new Dictionary<string, string>()
            {
                {"DnaTest1", "ACTA"},
                {"DnaTest2", "ACTAACTA"}
            };

            var actualDna = sequenceFactory.CreateLabelledSequences(testCase, SequenceType.Dna);
            
            Assert.IsInstanceOfType(actualDna, typeof(List<LabelledSequence>));
            CollectionAssert.AllItemsAreInstancesOfType(actualDna, typeof(Dna));
        }

        [TestMethod()]
        public void CreateProteinInstancesTest()
        {
            var sequenceFactory = CreateSequenceFactory();
            var testCase = new Dictionary<string, string>()
            {
                {"ProteinTest1", "PTRS"},
                {"ProteinTest2", "PSRTCF"}
            };

            var actualProtein = sequenceFactory.CreateLabelledSequences(testCase, SequenceType.Protein);

            Assert.IsInstanceOfType(actualProtein, typeof(List<LabelledSequence>));
            CollectionAssert.AllItemsAreInstancesOfType(actualProtein, typeof(Protein));
        }

        [TestMethod()]
        public void CreateMRnaInstancesTest()
        {
            var sequenceFactory = CreateSequenceFactory();
            var testCase = new Dictionary<string, string>()
            {
                {"MRnaTest1", "AUCG"},
                {"MRnaTest2", "CGUAC"}
            };

            var actualMRna = sequenceFactory.CreateLabelledSequences(testCase, SequenceType.MRna);

            Assert.IsInstanceOfType(actualMRna, typeof(List<LabelledSequence>));
            CollectionAssert.AllItemsAreInstancesOfType(actualMRna, typeof(MRna));
        }

        private ISequenceFactory CreateSequenceFactory()
        {
            ISequenceFactory sequenceFactory = new SequenceFactory();
            return sequenceFactory;
        }
    }
}