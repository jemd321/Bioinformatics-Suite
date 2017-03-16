using System.Collections.Generic;
using BioinformaticsSuite.Module.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Services
{
    [TestClass]
    public class EmblParserTests
    {
        private const string EmblFile =
            "ID   AB000263; SV 1; linear; mRNA; STD; HUM; 368 BP.\r\nXX\r\nAC   AB000263;\r\nXX\r\nDE   Human mRNA for prepro cortistatin like peptide, complete cds.\r\nXX\r\nSQ   Sequence 368 BP; 79 A; 123 C; 105 G; 61 T; 0 other;\r\n     acaagatgcc attgtccccc ggcctcctgc tgctgctgct ctccggggcc acggccaccg        60\r\n//ID   AB000263; SV 1; linear; mRNA; STD; HUM; 368 BP.\r\nXX\r\nAC   AB000262;\r\nXX\r\nDE   Human mRNA for prepro cortistatin like peptide, complete cds.\r\nXX\r\nSQ   Sequence 368 BP; 79 A; 123 C; 105 G; 61 T; 0 other;\r\n     acaagatgcc attgtccccc ggcctcctgc tgctgctgct ctccggggcc acggccaccc        60\r\n//";

        [TestMethod]
        public void TryParseEmblFileTest()
        {
            var emblParser = SetupMock();
            var expectedRecords = new List<string>
            {
                "ID   AB000263; SV 1; linear; mRNA; STD; HUM; 368 BP.\r\nXX\r\nAC   AB000263;\r\nXX\r\nDE   Human mRNA for prepro cortistatin like peptide, complete cds.\r\nXX\r\nSQ   Sequence 368 BP; 79 A; 123 C; 105 G; 61 T; 0 other;\r\n     acaagatgcc attgtccccc ggcctcctgc tgctgctgct ctccggggcc acggccaccg        60\r\n",
                "ID   AB000263; SV 1; linear; mRNA; STD; HUM; 368 BP.\r\nXX\r\nAC   AB000262;\r\nXX\r\nDE   Human mRNA for prepro cortistatin like peptide, complete cds.\r\nXX\r\nSQ   Sequence 368 BP; 79 A; 123 C; 105 G; 61 T; 0 other;\r\n     acaagatgcc attgtccccc ggcctcctgc tgctgctgct ctccggggcc acggccaccc        60\r\n"
            };
            var isValid = emblParser.TryParseEmblFile(EmblFile);
            var errorMessage = emblParser.ErrorMessage;
            var records = emblParser.EmblRecords;

            Assert.IsTrue(isValid);
            Assert.AreEqual("", errorMessage);
            CollectionAssert.AreEqual(expectedRecords, records);
        }

        [TestMethod]
        public void ResetSequencesTest()
        {
            const string badEmbl = ">AFASFSFASD DESCRIPTION AWDAWDAWD ORIGIN";
            var emblParser = SetupMock();

            emblParser.TryParseEmblFile(badEmbl);
            emblParser.ResetSequences();
            var errorMessage = emblParser.ErrorMessage;
            var records = emblParser.EmblRecords;

            Assert.AreEqual("", errorMessage);
            Assert.AreEqual(0, records.Count);

            emblParser.TryParseEmblFile(EmblFile);
            emblParser.ResetSequences();
            errorMessage = emblParser.ErrorMessage;
            records = emblParser.EmblRecords;

            Assert.AreEqual("", errorMessage);
            Assert.AreEqual(0, records.Count);
        }

        private IEmblParser SetupMock()
        {
            return new EmblParser();
        }
    }
}