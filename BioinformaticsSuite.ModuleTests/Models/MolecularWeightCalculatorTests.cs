using BioinformaticsSuite.Module.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Models
{
    [TestClass()]
    public class MolecularWeightCalculatorTests
    {
        [TestMethod()]
        public void CalculateMolecularWeightTest()
        {
            var weightCalculator = CreateTestInstance();

            var testDna = new Dna("test", "ACGT");
            var testRna = new Rna("test", "ACGU");

            const decimal expectedDnaWeight = 1173.84m;
            const decimal expectedRnaWeight = 1444.77m;

            weightCalculator.CalculateMolecularWeight(testDna);
            weightCalculator.CalculateMolecularWeight(testRna);

            var actualDnaWeight = testDna.MolecularWeight;
            var actualRnaWeight = testRna.MolecularWeight;

            Assert.AreEqual(expectedDnaWeight, actualDnaWeight);
            Assert.AreEqual(expectedRnaWeight, actualRnaWeight);
        }

        private IMolecularWeightCalculator CreateTestInstance()
        {
            IMolecularWeightCalculator weightCalculator = new MolecularWeightCalculator();
            return weightCalculator;
        }
    }
}