using BioinformaticsSuite.Module.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Models
{
    [TestClass]
    public class TranslatorTests
    {
        [TestMethod]
        public void TranslateDnaTest()
        {
            string testCase = "ACTGTATTA";
            string expected = "TVL";

            string actual = Translation.TranslateDnaToProtein(testCase);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TranslateOddCodonsTest()
        {
            string testCase = "ACTGTATTAA";
            string expected = "TVL";

            string actual = Translation.TranslateDnaToProtein(testCase);

            Assert.AreEqual(expected, actual);
        }
    }
}