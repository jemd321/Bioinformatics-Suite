using System.Collections.Generic;
using BioinformaticsSuite.Module.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Services
{
    [TestClass]
    public class GenbankParserTests
    {
        private const string GenbankFile =
            "LOCUS       SCU49845     5028 bp    DNA             PLN       21-JUN-1999\r\nDEFINITION  Saccharomyces cerevisiae TCP1-beta gene, partial cds, and Axl2p\r\n            (AXL2) and Rev7p (REV7) genes, complete cds.\r\nACCESSION   U49845\r\nVERSION     U49845.1  GI:1293613\r\nKEYWORDS    .\r\n\r\nORIGIN\r\n        1 gatcctccat atacaacggt atctccacct caggtttaga tctcaacaac ggaaccattc\r\n\r\n//LOCUS       SCU49845     5028 bp    DNA             PLN       21-JUN-1999\r\nDEFINITION  Saccharomyces cerevisiae TCP1-beta gene, partial cds, and Axl2p\r\n            (AXL2) and Rev7p (REV7) genes, complete cds.\r\nACCESSION   B49845\r\nVERSION     B49845.1  GI:1293613\r\nKEYWORDS    .\r\n\r\nORIGIN\r\n        1 gatcctccat atacaacggt atctccacct caggtttaga tctcaacaac ggaaccattg\r\n\r\n//";

        [TestMethod]
        public void TryParseGenbankFileTest()
        {
            var genbankParser = SetupMock();
            var expectedRecords = new List<string>
            {
                "LOCUS       SCU49845     5028 bp    DNA             PLN       21-JUN-1999\r\nDEFINITION  Saccharomyces cerevisiae TCP1-beta gene, partial cds, and Axl2p\r\n            (AXL2) and Rev7p (REV7) genes, complete cds.\r\nACCESSION   U49845\r\nVERSION     U49845.1  GI:1293613\r\nKEYWORDS    .\r\n\r\nORIGIN\r\n        1 gatcctccat atacaacggt atctccacct caggtttaga tctcaacaac ggaaccattc\r\n\r\n",
                "LOCUS       SCU49845     5028 bp    DNA             PLN       21-JUN-1999\r\nDEFINITION  Saccharomyces cerevisiae TCP1-beta gene, partial cds, and Axl2p\r\n            (AXL2) and Rev7p (REV7) genes, complete cds.\r\nACCESSION   B49845\r\nVERSION     B49845.1  GI:1293613\r\nKEYWORDS    .\r\n\r\nORIGIN\r\n        1 gatcctccat atacaacggt atctccacct caggtttaga tctcaacaac ggaaccattg\r\n\r\n"
            };
            var isValid = genbankParser.TryParseGenbankFile(GenbankFile);
            var errorMessage = genbankParser.ErrorMessage;
            var records = genbankParser.GenbankRecords;

            Assert.IsTrue(isValid);
            Assert.AreEqual("", errorMessage);
            CollectionAssert.AreEqual(expectedRecords, records);
        }

        [TestMethod]
        public void ResetSequencesTest()
        {
            const string badEmbl = ">AFASFSFASD ID AWDAWDAWD AC";
            var genbankParser = SetupMock();

            genbankParser.TryParseGenbankFile(badEmbl);
            genbankParser.ResetSequences();
            var errorMessage = genbankParser.ErrorMessage;
            var records = genbankParser.GenbankRecords;

            Assert.AreEqual("", errorMessage);
            Assert.AreEqual(0, records.Count);

            genbankParser.TryParseGenbankFile(GenbankFile);
            genbankParser.ResetSequences();
            errorMessage = genbankParser.ErrorMessage;
            records = genbankParser.GenbankRecords;

            Assert.AreEqual("", errorMessage);
            Assert.AreEqual(0, records.Count);
        }

        private IGenbankParser SetupMock()
        {
            return new GenbankParser();
        }
    }
}