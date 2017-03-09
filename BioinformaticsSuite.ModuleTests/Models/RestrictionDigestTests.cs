using System.Collections.Generic;
using System.Linq;
using BioinformaticsSuite.Module.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Models
{
    [TestClass()]
    public class RestrictionDigestTests
    {
        [TestMethod()]
        public void TestOneEnzymeDigest()
        {
            var restrictionDigest = CreateTestInstance();
            var enzymes = new List<string>() {"CC|GG"};
            var testSequence = new List<LabelledSequence>() {new Dna("test", "ACGTCCGGTGCACCGGCCGGACGT")};
        
            var expectedFragments = new List<DigestFragment>() {new DigestFragment("CC|GG", 0, "ACGTCC"), new DigestFragment("CC|GG", 6, "GGTGCACC"), new DigestFragment("CC|GG", 14, "GGCC"), new DigestFragment("CC|GG", 18, "GGACGT") };
            var expectedLabelledFragments = new List<LabelledDigestFragments>() {new LabelledDigestFragments("test", expectedFragments)};

            var actualLabelledFragments = restrictionDigest.FindRestrictionDigestFragments(enzymes, testSequence);

            for (int i = 0; i < expectedFragments.Count; i++)
            {
                Assert.AreEqual(expectedFragments[i].Enzyme, actualLabelledFragments.First().DigestFramgments[i].Enzyme);
                Assert.AreEqual(expectedFragments[i].CutPosition, actualLabelledFragments.First().DigestFramgments[i].CutPosition);
                Assert.AreEqual(expectedFragments[i].Fragment, actualLabelledFragments.First().DigestFramgments[i].Fragment);
            }
        }

        [TestMethod()]
        public void TestThreeEnzymeDigest()
        {
            var restrictionDigest = CreateTestInstance();
            var enzymes = new List<string>() { "CC|GG", "AC|GT", "GT|CC" };
            var testSequence = new List<LabelledSequence>() { new Dna("test", "ACGTCCGGTGCACCGGCCGGACGT") };

            var expectedFragments = new List<DigestFragment>() { new DigestFragment("CC|GG", 0, "AC"), new DigestFragment("AC|GT", 2, "GT"),new DigestFragment("GT|CC", 4, "CC"), new DigestFragment("CC|GG", 6, "GGTGCACC"), new DigestFragment("CC|GG", 14, "GGCC"), new DigestFragment("CC|GG", 18, "GGAC"), new DigestFragment("AC|GT", 22, "GT")};
            var expectedLabelledFragments = new List<LabelledDigestFragments>() { new LabelledDigestFragments("test", expectedFragments) };

            var actualLabelledFragments = restrictionDigest.FindRestrictionDigestFragments(enzymes, testSequence);

            for (int i = 0; i < expectedFragments.Count; i++)
            {
                Assert.AreEqual(expectedFragments[i].Enzyme, actualLabelledFragments.First().DigestFramgments[i].Enzyme);
                Assert.AreEqual(expectedFragments[i].CutPosition, actualLabelledFragments.First().DigestFramgments[i].CutPosition);
                Assert.AreEqual(expectedFragments[i].Fragment, actualLabelledFragments.First().DigestFramgments[i].Fragment);
            }
        }

        private IRestrictionDigest CreateTestInstance()
        {
            return new RestrictionDigest();
        }
    }
}