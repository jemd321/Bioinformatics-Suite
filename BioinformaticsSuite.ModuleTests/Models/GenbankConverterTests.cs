using System.Collections.Generic;
using System.Linq;
using BioinformaticsSuite.Module.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.Models
{
    [TestClass()]
    public class GenbankConverterTests
    {
        [TestMethod()]
        public void ConvertGenbankFastaDnaTest()
        {
            var genbankConverter = SetupMock();
            string testString =
                "LOCUS       BC035912                2074 bp    mRNA    linear   PRI 19-JAN-2006\nDEFINITION  Homo sapiens dipeptidyl-peptidase 6, mRNA (cDNA clone\n            IMAGE:5494573), complete cds.\nACCESSION   BC035912\nVERSION     BC035912.1\nKEYWORDS    " +
                "ORIGIN      \n        1 ccacgcgtcc gggtggtgcc aaattctggg gcctaggcat ttccctcgct ttatgttttt\n//";
            var testCase = new List<string> {testString};
            string expectedlabel = ">BC035912.1 Homo sapiens dipeptidyl-peptidase 6, mRNA (cDNA clone IMAGE:5494573), complete cds.";
            string expectedSequence = "CCACGCGTCCGGGTGGTGCCAAATTCTGGGGCCTAGGCATTTCCCTCGCTTTATGTTTTT";

            var labelledFasta = genbankConverter.ConvertGenbankFastaDna(testCase);
            var actualLabel = labelledFasta.FirstOrDefault().Key;
            var actualSequence = labelledFasta.FirstOrDefault().Value;
       
            Assert.AreEqual(expectedlabel, actualLabel);
            Assert.AreEqual(expectedSequence, actualSequence);
        }

        [TestMethod()]
        public void ConvertGenbankFastaProteinTest()
        {
            var genbankConverter = SetupMock();
            string testString =
                "LOCUS       BC035912                2074 bp    mRNA    linear   PRI 19-JAN-2006\nDEFINITION  Homo sapiens dipeptidyl-peptidase 6, mRNA (cDNA clone\n            IMAGE:5494573), complete cds.\nACCESSION   BC035912\nVERSION     BC035912.1\nKEYWORDS    " +
                "ORIGIN      \n        1 ccacgcgtcc gggtggtgcc aaattctggg gcctaggcat ttccctcgct ttatgttttt\n//";
            List<string> testCase = new List<string>() { testString };
            string expectedlabel = ">BC035912.1 Homo sapiens dipeptidyl-peptidase 6, mRNA (cDNA clone IMAGE:5494573), complete cds.";
            string expectedSequence = "PRVRVVPNSGAXAFPSLYVF";

            var labelledFasta = genbankConverter.ConvertGenbankFastaProtein(testCase);
            var actualLabel = labelledFasta.FirstOrDefault().Key;
            var actualSequence = labelledFasta.FirstOrDefault().Value;

            Assert.AreEqual(expectedlabel, actualLabel);
            Assert.AreEqual(expectedSequence, actualSequence);
        }


        private IGenbankConverter SetupMock()
        {
            return new GenbankConverter();
        }
    }
}