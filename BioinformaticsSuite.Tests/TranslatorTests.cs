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
    public class TranslatorTests
    {
        [TestMethod()]
        public void TranslateDnaTest()
        {
            string testCase = "ACTGTATTA";
            string expected = "TVL";

            string actual = Translator.TranslateDna(testCase);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TranslateOddCodonsTest()
        {
            string testCase = "ACTGTATTAA";
            string expected = "TVL";

            string actual = Translator.TranslateDna(testCase);

            Assert.AreEqual(expected, actual);
        }
    }

}