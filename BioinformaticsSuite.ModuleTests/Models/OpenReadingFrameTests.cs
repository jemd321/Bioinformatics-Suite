using System.Collections.Generic;
using System.Linq;
using BioinformaticsSuite.Module.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Models
{
    [TestClass]
    public class OpenReadingFrameTests
    {
        //MTVLMTVLTVL*
        [TestMethod()]
        public void OpenReadingFrameTest()
        {
            var orfFinder = SetupMock();
            var testCase = new Dna(">test", "ATGACTGTATTAATGACTGTATTAACTGTATTATAG");
            var expectedOrfs = new Dictionary<string, string>()
            {
                {">test: 1-11 length: 11", "MTVLMTVLTVL"},
                {">test: 5-11 length: 7", "MTVLTVL" }
            };

            var actualOrfs = orfFinder.FindOpenReadingFrames(testCase);

            Assert.AreEqual(2, actualOrfs.Count);
            CollectionAssert.AreEquivalent(expectedOrfs, actualOrfs);
        }

        private static IOpenReadingFrameFinder SetupMock()
        {
            return new OpenReadingFrameFinder(new ReadingFrameFactory());
        }
        
    }
}