using System.Linq;
using System.Security.AccessControl;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.ViewModels;
using ICSharpCode.AvalonEdit.Editing;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioinformaticsSuite.ModuleTests.ViewModels
{
    [TestClass]
    public class ViewModelTests
    {
        #region Base SequenceViewModel Tests
        [TestMethod]
        public void OnClearTest()
        {
            var mockViewModel = SetUpMock();
            mockViewModel.InputBoxText = "Test";
            const string expectedTextBoxInput = "";

            mockViewModel.OnClear();

            Assert.AreEqual(expectedTextBoxInput, mockViewModel.InputBoxText);
        }

        [TestMethod]
        public void OnLoadTest()
        {
            //Not easily testable until refactored away from Win32 Dialog boxes.
            Assert.Fail();
        }

        [TestMethod]
        public void OnSaveTest()
        {
            //Not easily testable until refactored away from Win32 Dialog boxes.
            Assert.Fail();
        }

        private static MockViewModel SetUpMock()
        {
            return new MockViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()));
        }
        #endregion

        #region Conversion View Model Tests

        [TestMethod()]
        public void ConversionEmblFastaViewModelTest()
        {
            var viewModel = new ConversionEmblFastaViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new EmblConverter(), new EmblParser());
            const string testCase = "ID   AB000263; SV 1; linear; mRNA; STD; HUM; 368 BP.\r\nXX\r\nAC   AB000263;\r\nXX\r\nDT   24-APR-1997 (Rel. 51, Created)\r\nDT   25-APR-1997 (Rel. 51, Last updated, Version 2)\r\nXX\r\nDE   Human mRNA for prepro cortistatin like peptide, complete cds.\r\nXX\r\nKW   prepro cortistatin like peptide.\r\nXX\r\nOS   Homo sapiens (human)\r\nOC   Eukaryota; Metazoa; Chordata; Craniata; Vertebrata; Euteleostomi; Mammalia;\r\nOC   Eutheria; Euarchontoglires; Primates; Haplorrhini; Catarrhini; Hominidae;\r\nOC   Homo.\r\nXX\r\nRN   [1]\r\nRP   1-368\r\nRA   Fukusumi S.;\r\nRT   ;\r\nRL   Submitted (06-JAN-1997) to the INSDC.\r\nRL   Shoji Fukusumi, Takeda Chemical Ind. Ltd., Discovery Reserch Laboratories\r\nRL   I; Wadai 10, Tsukuba, Ibaraki 300-42, Japan (Tel:0298-64-5039,\r\nRL   Fax:0298-64-5000)\r\nXX\r\nRN   [2]\r\nRP   1-368\r\nRA   Fukusumi S.;\r\nRT   \"Identification and characterization of a human novel cortistatin like\r\nRT   peptide\";\r\nRL   Unpublished.\r\nXX\r\nRN   [3]\r\nRX   DOI; 10.1006/bbrc.1997.6252.\r\nRX   PUBMED; 9125122.\r\nRA   Fukusumi S., Kitada C., Takekawa S., Kizawa H., Sakamoto J., Miyamoto M.,\r\nRA   Hinuma S., Kitano K., Fujino M.;\r\nRT   \"Identification and characterization of a novel human cortistatin-like\r\nRT   peptide\";\r\nRL   Biochem. Biophys. Res. Commun. 232(1):157-163(1997).\r\nXX\r\nDR   MD5; 54bd01d8d516dede30b85ab01481ca01.\r\nDR   Ensembl-Gn; ENSG00000241563; homo_sapiens.\r\nDR   Ensembl-Tr; ENST00000377049; homo_sapiens.\r\nXX\r\nFH   Key             Location/Qualifiers\r\nFH\r\nFT   source          1..368\r\nFT                   /organism=\"Homo sapiens\"\r\nFT                   /mol_type=\"mRNA\"\r\nFT                   /clone=\"phCSP6\"\r\nFT                   /tissue_type=\"Brain\"\r\nFT                   /db_xref=\"taxon:9606\"\r\nFT   CDS             6..323\r\nFT                   /product=\"prepro cortistatin like peptide\"\r\nFT                   /db_xref=\"GOA:O00230\"\r\nFT                   /db_xref=\"H-InvDB:HIT000057896.12\"\r\nFT                   /db_xref=\"HGNC:HGNC:2257\"\r\nFT                   /db_xref=\"InterPro:IPR004250\"\r\nFT                   /db_xref=\"InterPro:IPR018142\"\r\nFT                   /db_xref=\"UniProtKB/Swiss-Prot:O00230\"\r\nFT                   /protein_id=\"BAA19770.1\"\r\nFT                   /translation=\"MPLSPGLLLLLLSGATATAALPLEGGPTGRDSEHMQEAAGIRKSS\r\nFT                   LLTFLAWWFEWTSQASAGPLIGEEAREVARRQEGAPPQQSARRDRMPCRNFFWKTFSSC\r\nFT                   K\"\r\nXX\r\nSQ   Sequence 368 BP; 79 A; 123 C; 105 G; 61 T; 0 other;\r\n     acaagatgcc attgtccccc ggcctcctgc tgctgctgct ctccggggcc acggccaccg        60\r\n     ctgccctgcc cctggagggt ggccccaccg gccgagacag cgagcatatg caggaagcgg       120\r\n     caggaataag gaaaagcagc ctcctgactt tcctcgcttg gtggtttgag tggacctccc       180\r\n     aggccagtgc cgggcccctc ataggagagg aagctcggga ggtggccagg cggcaggaag       240\r\n     gcgcaccccc ccagcaatcc gcgcgccggg acagaatgcc ctgcaggaac ttcttctgga       300\r\n     agaccttctc ctcctgcaaa taaaacctca cccatgaatg ctcacgcaag tttaattaca       360\r\n     gacctgaa                                                                368\r\n//\r\n";
            const string expectedResult = ">B000263|Human mRNA for prepro cortistatin like peptide, complete cds.\r\nACAAGATGCCATTGTCCCCCGGCCTCCTGCTGCTGCTGCTCTCCGGGGCCACGGCCACCGCTGCCCTGCCCCTGGAGGGTGGCCCCACCGGCCGAGACAGCGAGCATATGCAGGAAGCGGCAGGAATAAGGAAAAGCAGCCTCCTGACTTTCCTCGCTTGGTGGTTTGAGTGGACCTCCCAGGCCAGTGCCGGGCCCCTCATAGGAGAGGAAGCTCGGGAGGTGGCCAGGCGGCAGGAAGGCGCACCCCCCCAGCAATCCGCGCGCCGGGACAGAATGCCCTGCAGGAACTTCTTCTGGAAGACCTTCTCCTCCTGCAAATAAAACCTCACCCATGAATGCTCACGCAAGTTTAATTACAGACCTGAA\r\n";
            viewModel.InputBoxText = testCase;
            
            viewModel.OnRun();
            var actualResult = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void ConversionEmblTranslateViewModelTest()
        {
            var viewModel = new ConversionEmblTranslateViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new EmblConverter(), new EmblParser());
            const string testCase = "ID   AB000263; SV 1; linear; mRNA; STD; HUM; 368 BP.\r\nXX\r\nAC   AB000263;\r\nXX\r\nDT   24-APR-1997 (Rel. 51, Created)\r\nDT   25-APR-1997 (Rel. 51, Last updated, Version 2)\r\nXX\r\nDE   Human mRNA for prepro cortistatin like peptide, complete cds.\r\nXX\r\nKW   prepro cortistatin like peptide.\r\nXX\r\nOS   Homo sapiens (human)\r\nOC   Eukaryota; Metazoa; Chordata; Craniata; Vertebrata; Euteleostomi; Mammalia;\r\nOC   Eutheria; Euarchontoglires; Primates; Haplorrhini; Catarrhini; Hominidae;\r\nOC   Homo.\r\nXX\r\nRN   [1]\r\nRP   1-368\r\nRA   Fukusumi S.;\r\nRT   ;\r\nRL   Submitted (06-JAN-1997) to the INSDC.\r\nRL   Shoji Fukusumi, Takeda Chemical Ind. Ltd., Discovery Reserch Laboratories\r\nRL   I; Wadai 10, Tsukuba, Ibaraki 300-42, Japan (Tel:0298-64-5039,\r\nRL   Fax:0298-64-5000)\r\nXX\r\nRN   [2]\r\nRP   1-368\r\nRA   Fukusumi S.;\r\nRT   \"Identification and characterization of a human novel cortistatin like\r\nRT   peptide\";\r\nRL   Unpublished.\r\nXX\r\nRN   [3]\r\nRX   DOI; 10.1006/bbrc.1997.6252.\r\nRX   PUBMED; 9125122.\r\nRA   Fukusumi S., Kitada C., Takekawa S., Kizawa H., Sakamoto J., Miyamoto M.,\r\nRA   Hinuma S., Kitano K., Fujino M.;\r\nRT   \"Identification and characterization of a novel human cortistatin-like\r\nRT   peptide\";\r\nRL   Biochem. Biophys. Res. Commun. 232(1):157-163(1997).\r\nXX\r\nDR   MD5; 54bd01d8d516dede30b85ab01481ca01.\r\nDR   Ensembl-Gn; ENSG00000241563; homo_sapiens.\r\nDR   Ensembl-Tr; ENST00000377049; homo_sapiens.\r\nXX\r\nFH   Key             Location/Qualifiers\r\nFH\r\nFT   source          1..368\r\nFT                   /organism=\"Homo sapiens\"\r\nFT                   /mol_type=\"mRNA\"\r\nFT                   /clone=\"phCSP6\"\r\nFT                   /tissue_type=\"Brain\"\r\nFT                   /db_xref=\"taxon:9606\"\r\nFT   CDS             6..323\r\nFT                   /product=\"prepro cortistatin like peptide\"\r\nFT                   /db_xref=\"GOA:O00230\"\r\nFT                   /db_xref=\"H-InvDB:HIT000057896.12\"\r\nFT                   /db_xref=\"HGNC:HGNC:2257\"\r\nFT                   /db_xref=\"InterPro:IPR004250\"\r\nFT                   /db_xref=\"InterPro:IPR018142\"\r\nFT                   /db_xref=\"UniProtKB/Swiss-Prot:O00230\"\r\nFT                   /protein_id=\"BAA19770.1\"\r\nFT                   /translation=\"MPLSPGLLLLLLSGATATAALPLEGGPTGRDSEHMQEAAGIRKSS\r\nFT                   LLTFLAWWFEWTSQASAGPLIGEEAREVARRQEGAPPQQSARRDRMPCRNFFWKTFSSC\r\nFT                   K\"\r\nXX\r\nSQ   Sequence 368 BP; 79 A; 123 C; 105 G; 61 T; 0 other;\r\n     acaagatgcc attgtccccc ggcctcctgc tgctgctgct ctccggggcc acggccaccg        60\r\n     ctgccctgcc cctggagggt ggccccaccg gccgagacag cgagcatatg caggaagcgg       120\r\n     caggaataag gaaaagcagc ctcctgactt tcctcgcttg gtggtttgag tggacctccc       180\r\n     aggccagtgc cgggcccctc ataggagagg aagctcggga ggtggccagg cggcaggaag       240\r\n     gcgcaccccc ccagcaatcc gcgcgccggg acagaatgcc ctgcaggaac ttcttctgga       300\r\n     agaccttctc ctcctgcaaa taaaacctca cccatgaatg ctcacgcaag tttaattaca       360\r\n     gacctgaa                                                                368\r\n//\r\n";
            const string expectedResult = ">B000263|Human mRNA for prepro cortistatin like peptide, complete cds.\r\nTRCHCPPASCCCCSPGPRPPLPCPWRVAPPAETASICRKRQE*GKAAS*LSSLGGLSGPPRPVPGPS*ERKLGRWPGGRKAHPPSNPRAGTECPAGTSSGRPSPPANKTSPMNAHASLITDL\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actualResult = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void ConversionGenbankFastaViewModelTest()
        {
            var viewModel = new ConversionGenbankFastaViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new GenbankConverter(), new GenbankParser());
            const string testCase = "LOCUS       BC035912                2074 bp    mRNA    linear   PRI 19-JAN-2006\nDEFINITION  Homo sapiens dipeptidyl-peptidase 6, mRNA (cDNA clone\n            IMAGE:5494573), complete cds.\nACCESSION   BC035912\nVERSION     BC035912.1\nKEYWORDS    ORIGIN      \n        1 ccacgcgtcc gggtggtgcc aaattctggg gcctaggcat ttccctcgct ttatgttttt\n//";
            const string expectedResult = ">BC035912.1 Homo sapiens dipeptidyl-peptidase 6, mRNA (cDNA clone IMAGE:5494573), complete cds.\r\nCCACGCGTCCGGGTGGTGCCAAATTCTGGGGCCTAGGCATTTCCCTCGCTTTATGTTTTT\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actualResult = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void ConversionGenbankTranslateViewModelTest()
        {
            var viewModel = new ConversionGenbankTranslateViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new GenbankConverter(), new GenbankParser());
            const string testCase = "LOCUS       BC035912                2074 bp    mRNA    linear   PRI 19-JAN-2006\nDEFINITION  Homo sapiens dipeptidyl-peptidase 6, mRNA (cDNA clone\n            IMAGE:5494573), complete cds.\nACCESSION   BC035912\nVERSION     BC035912.1\nKEYWORDS    ORIGIN      \n        1 ccacgcgtcc gggtggtgcc aaattctggg gcctaggcat ttccctcgct ttatgttttt\n//";
            const string expectedResult = ">BC035912.1 Homo sapiens dipeptidyl-peptidase 6, mRNA (cDNA clone IMAGE:5494573), complete cds.\r\nPRVRVVPNSGA*AFPSLYVF\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actualResult = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void ConversionSplitFastaViewModelTest()
        {
            var viewModel = new ConversionFastaSplitViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new FastaManipulator());
            const string testCase = ">test\r\nACGTCCGGTGCACCGGCCGGACGT";
            const string expectedResult = ">fragment_1;test;start=1;end=12;length=12;source_length=24;\r\nACGTCCGGTGCA\r\n>fragment_2;test;start=13;end=24;length=12;source_length=24;\r\nCCGGCCGGACGT\r\n";
            viewModel.InputBoxText = testCase;
            viewModel.SequenceLengthBoxText = "12";

            viewModel.OnRun();
            var actualResult = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void ConversionCombineFastaViewModelTest()
        {
            var viewModel = new ConversionFastaCombineViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new FastaManipulator());
            const string testCase = ">test1\r\nACGTCCGGTGCACCGGCCGGACGT\r\n>test2\r\nCCGGTGCACCGGCCGG";
            const string expectedResult = ">40 base sequence from 2 sequences\r\nACGTCCGGTGCACCGGCCGGACGTCCGGTGCACCGGCCGG\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actualResult = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actualResult);
        }

        #endregion

        #region Dna View Model Tests

        [TestMethod()]
        public void DnaFindMotifViewModelTest()
        {
            var viewModel = new DnaFindMotifViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new MotifFinder());
            var testCase = ">test\r\nACGTGTCAACGT";
            var expectedResult = ">test\r\n1 - 4    9 - 12    \r\n";
            viewModel.MotifBoxText = "ACGT";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void DnaMolecularWeightViewModelTest()
        {
            var viewModel = new DnaMolecularWeightViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new MolecularWeightCalculator());
            const string testCase = ">test\r\nACGTTGCAACGT";
            const string expectedResult = ">test\r\n3.645 kDa\r\n";

            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void DnaReadingFrameViewModelTest()
        {
            var viewModel = new DnaReadingFrameViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new ReadingFrameFactory());
            const string testCase = ">Test\r\nACTGTGAC";
            const string expectedResult = ">Test +1\r\nACTGTG\r\n>Test +2\r\nCTGTGA\r\n>Test +3\r\nTGTGAC\r\n>Test -1\r\nGTCACA\r\n>Test -2\r\nTCACAG\r\n>Test -3\r\nCACAGT\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actualResult = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void DnaRestricitionDigestViewModelTest()
        {
            var viewModel = new DnaRestricitionDigestViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new RestrictionDigest());
            const string testCase = ">test\r\nATCCGCGTCAGTACTGAAGCTAAC";
            string[] testEnzymes = { "CG|CG", "GT|AC", "AG|CT" };
            const string expectedResult = ">test;Fragment: 5-12;Enzyme: CG|CG;Length: 7;\r\nCGTCAGT\r\n>test;Fragment: 12-19;Enzyme: GT|AC;Length: 7;\r\nACTGAAG\r\n>test;Fragment: 0-5;Enzyme: CG|CG;Length: 5;\r\nATCCG\r\n>test;Fragment: 19-24;Enzyme: AG|CT;Length: 5;\r\nCTAAC\r\n";

            viewModel.InputBoxText = testCase;
            viewModel.EnzymeBox1Selection = testEnzymes[0];
            viewModel.EnzymeBox2Selection = testEnzymes[1];
            viewModel.EnzymeBox3Selection = testEnzymes[2];

            viewModel.OnRun();
            var actualResult = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void DnaStatisticsViewModelTest()
        {
            var viewModel = new DnaStatisticsViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()));
            const string testCase = ">test\r\nACGTTGCAACGT";
            const string expectedResult =
                ">test\r\nSequenceLength: 12    Base Count:  A:   3   C:   3   G:   3   T:   3    Base Percent:   A:   25.00%   C:   25.00%   G:   25.00%   T:   25.00%\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void DnaTranscribeViewModelTest()
        {
            var viewModel = new DnaTranscribeViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()));
            const string testCase = ">test\r\nACGTTGCAACGT";
            const string expectedResult = ">test\r\nACGUUGCAACGU\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void DnaTranslateViewModelTest()
        {
            var viewModel = new DnaTranslateViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()));
            const string testCase = ">test\r\nACGTTGCAACGT";
            const string expectedResult = ">test\r\nTLQR\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }


        #endregion

        #region Rna View Model Tests

        [TestMethod()]
        public void RnaMolecularWeightViewModelTest()
        {
            var viewModel = new RnaMolecularWeightViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new MolecularWeightCalculator());
            const string testCase = ">test\r\nACGUUGCAACGU";
            const string expectedResult = ">test\r\n4.016 kDa\r\n";

            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void RnaTranslateViewModelTest()
        {
            var viewModel = new RnaTranslateViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()));
            const string testCase = ">test\r\nACGUUGCAACGU";
            const string expectedResult = ">test\r\nTLQR\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }

        #endregion

        #region Protein View Model Tests

        [TestMethod()]
        public void ProteinMolecularWeightViewModelTest()
        {
            var viewModel = new ProteinMolecularWeightViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()), new MolecularWeightCalculator());
            const string testCase = ">test\r\nMPR*";
            const string expectedResult = ">test\r\n0.403 kDa\r\n";

            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void ProteinOpenReadingFramesViewModelTest()
        {
            var viewModel = new ProteinOpenReadingFrameViewModel(new SequenceFactory(),
                new FastaParser(new SequenceValidator()), new OpenReadingFrameFinder(new ReadingFrameFactory()));
            const string testCase = ">test\r\nATGACTGTATTAATGACTGTATTAACTGTATTATAG";
            const string expectedResult = ">test: 1-11 length: 11\r\nMTVLMTVLTVL\r\n>test: 5-11 length: 7\r\nMTVLTVL\r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void ProteinStatisticsViewModelTest()
        {
            var viewModel = new ProteinStatisticsViewModel(new SequenceFactory(), new FastaParser(new SequenceValidator()));
            const string testCase = ">test\r\nMPR*";
            const string expectedResult =
                ">test\r\nA: 0 C: 0 D: 0 E: 0 F: 0 G: 0 H: 0 I: 0 K: 0 L: 0 M: 1 N: 0 P: 1 Q: 0 R: 1 S: 0 T: 0 V: 0 W: 0 Y: 0 *: 1 \r\nA: 0% C: 0% D: 0% E: 0% F: 0% G: 0% H: 0% I: 0% K: 0% L: 0% M: 25.00% N: 0% P: 25.00% Q: 0% R: 25.00% S: 0% T: 0% V: 0% W: 0% Y: 0% *: 25.00% \r\n";
            viewModel.InputBoxText = testCase;

            viewModel.OnRun();
            var actual = viewModel.ResultBoxText;

            Assert.AreEqual(expectedResult, actual);
        }

        #endregion

    }
}