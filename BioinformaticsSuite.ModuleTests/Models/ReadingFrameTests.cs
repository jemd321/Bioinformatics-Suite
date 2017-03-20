using System.Collections.Generic;
using BioinformaticsSuite.Module.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Models
{
    [TestClass]
    public class ReadingFrameTests
    {
        [TestMethod]
        public void ReadingFrameTest()
        {
            var dna = new Dna(">test", "ACTGTGAC");
            var readingFrame = new ReadingFrame(dna);

            const string expectedLabel = ">test";
            var expectedFrames = new Dictionary<string, string>
            {
                {">test +1", "ACTGTG"},
                {">test +2", "CTGTGA"},
                {">test +3", "TGTGAC"},
                {">test -1", "GTCACA"},
                {">test -2", "TCACAG"},
                {">test -3", "CACAGT"}
            };

            var actualLabel = readingFrame.Label;
            var actualFrames = readingFrame.LabelledFrames;

            Assert.AreEqual(expectedLabel, actualLabel);
            CollectionAssert.AreEquivalent(expectedFrames, actualFrames);
        }
    }
}