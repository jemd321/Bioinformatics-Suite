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
    public class OpenReadingFrameTests
    {
        [TestMethod()]
        public void OpenReadingFrameTest()
        {
            var testProtein = new Protein(">test", "MLMPRSTX");
            var openReadingFrame = new OpenReadingFrame(testProtein);

            var expectedLabel = ">test";
            var expectedOrfs = new Dictionary<string, string>()
            {
                {">test 1-8", "MLMPRST"},
                {">test 3-8", "MPRST" }
            };

            Assert.AreEqual(expectedLabel, openReadingFrame.Label);
            CollectionAssert.AreEquivalent(expectedOrfs, openReadingFrame.OrfList);
        }
    }
}