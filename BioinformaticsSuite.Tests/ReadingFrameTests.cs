using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioinformaticsSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Models;

namespace BioinformaticsSuite.Tests
{
    [TestClass()]
    public class ReadingFrameTests
    {
        [TestMethod()]
        public void ReadingFrameTest()
        {
            Dna dna =  new Dna(">test", "ACTGTGAC");
            ReadingFrame readingFrame =  new ReadingFrame(dna);

            string expectedLabel = ">test";
            var expectedFrames = new Dictionary<string, string>()
            {
                {">test +1", "ACTGTG"},
                {">test +2", "CTGTGA"},
                {">test +3", "TGTGAC"},
                {">test -1", "GTCACA"},
                {">test -2", "TCACAG"},
                {">test -3", "CACAGT"},
            };

            var actualLabel = readingFrame.Label;
            var actualFrames = readingFrame.LabelledFrames;

            Assert.AreEqual(expectedLabel, actualLabel);
            CollectionAssert.AreEquivalent(expectedFrames,actualFrames);
        }
    }
}