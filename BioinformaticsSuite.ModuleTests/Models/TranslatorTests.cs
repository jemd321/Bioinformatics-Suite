using BioinformaticsSuite.Module.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Models
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