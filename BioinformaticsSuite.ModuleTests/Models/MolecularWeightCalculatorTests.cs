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
            var testProtein = new Protein("test", "ACDEFGHIKLMNPQRSTVWY*");

            const decimal expectedDnaWeight = 1.174m;
            const decimal expectedRnaWeight = 1.445m;
            const decimal expectedProteinWeight = 2.396m;

            weightCalculator.CalculateMolecularWeight(testDna);
            weightCalculator.CalculateMolecularWeight(testRna);
            weightCalculator.CalculateMolecularWeight(testProtein);

            var actualDnaWeight = testDna.MolecularWeight;
            var actualRnaWeight = testRna.MolecularWeight;
            var actualProteinWeight = testProtein.MolecularWeight;

            Assert.AreEqual(expectedDnaWeight, actualDnaWeight);
            Assert.AreEqual(expectedRnaWeight, actualRnaWeight);
            Assert.AreEqual(expectedProteinWeight, actualProteinWeight);
        }

        private IMolecularWeightCalculator CreateTestInstance()
        {
            IMolecularWeightCalculator weightCalculator = new MolecularWeightCalculator();
            return weightCalculator;
        }
    }
}