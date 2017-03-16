using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioinformaticsSuite.Module.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsSuite.Module.Models.Tests
{
    [TestClass()]
    public class EmblConverterTests
    {
        private readonly List<string> _testCase = new List<string>() { "ID   AB000263; SV 1; linear; mRNA; STD; HUM; 368 BP.\r\nXX\r\nAC   AB000263;\r\nXX\r\nDT   24-APR-1997 (Rel. 51, Created)\r\nDT   25-APR-1997 (Rel. 51, Last updated, Version 2)\r\nXX\r\nDE   Human mRNA for prepro cortistatin like peptide, complete cds.\r\nXX\r\nKW   prepro cortistatin like peptide.\r\nXX\r\nOS   Homo sapiens (human)\r\nOC   Eukaryota; Metazoa; Chordata; Craniata; Vertebrata; Euteleostomi; Mammalia;\r\nOC   Eutheria; Euarchontoglires; Primates; Haplorrhini; Catarrhini; Hominidae;\r\nOC   Homo.\r\nXX\r\nRN   [1]\r\nRP   1-368\r\nRA   Fukusumi S.;\r\nRT   ;\r\nRL   Submitted (06-JAN-1997) to the INSDC.\r\nRL   Shoji Fukusumi, Takeda Chemical Ind. Ltd., Discovery Reserch Laboratories\r\nRL   I; Wadai 10, Tsukuba, Ibaraki 300-42, Japan (Tel:0298-64-5039,\r\nRL   Fax:0298-64-5000)\r\nXX\r\nRN   [2]\r\nRP   1-368\r\nRA   Fukusumi S.;\r\nRT   \"Identification and characterization of a human novel cortistatin like\r\nRT   peptide\";\r\nRL   Unpublished.\r\nXX\r\nRN   [3]\r\nRX   DOI; 10.1006/bbrc.1997.6252.\r\nRX   PUBMED; 9125122.\r\nRA   Fukusumi S., Kitada C., Takekawa S., Kizawa H., Sakamoto J., Miyamoto M.,\r\nRA   Hinuma S., Kitano K., Fujino M.;\r\nRT   \"Identification and characterization of a novel human cortistatin-like\r\nRT   peptide\";\r\nRL   Biochem. Biophys. Res. Commun. 232(1):157-163(1997).\r\nXX\r\nDR   MD5; 54bd01d8d516dede30b85ab01481ca01.\r\nDR   Ensembl-Gn; ENSG00000241563; homo_sapiens.\r\nDR   Ensembl-Tr; ENST00000377049; homo_sapiens.\r\nXX\r\nFH   Key             Location/Qualifiers\r\nFH\r\nFT   source          1..368\r\nFT                   /organism=\"Homo sapiens\"\r\nFT                   /mol_type=\"mRNA\"\r\nFT                   /clone=\"phCSP6\"\r\nFT                   /tissue_type=\"Brain\"\r\nFT                   /db_xref=\"taxon:9606\"\r\nFT   CDS             6..323\r\nFT                   /product=\"prepro cortistatin like peptide\"\r\nFT                   /db_xref=\"GOA:O00230\"\r\nFT                   /db_xref=\"H-InvDB:HIT000057896.12\"\r\nFT                   /db_xref=\"HGNC:HGNC:2257\"\r\nFT                   /db_xref=\"InterPro:IPR004250\"\r\nFT                   /db_xref=\"InterPro:IPR018142\"\r\nFT                   /db_xref=\"UniProtKB/Swiss-Prot:O00230\"\r\nFT                   /protein_id=\"BAA19770.1\"\r\nFT                   /translation=\"MPLSPGLLLLLLSGATATAALPLEGGPTGRDSEHMQEAAGIRKSS\r\nFT                   LLTFLAWWFEWTSQASAGPLIGEEAREVARRQEGAPPQQSARRDRMPCRNFFWKTFSSC\r\nFT                   K\"\r\nXX\r\nSQ   Sequence 368 BP; 79 A; 123 C; 105 G; 61 T; 0 other;\r\n     acaagatgcc attgtccccc ggcctcctgc tgctgctgct ctccggggcc acggccaccg        60\r\n     ctgccctgcc cctggagggt ggccccaccg gccgagacag cgagcatatg caggaagcgg       120\r\n     caggaataag gaaaagcagc ctcctgactt tcctcgcttg gtggtttgag tggacctccc       180\r\n     aggccagtgc cgggcccctc ataggagagg aagctcggga ggtggccagg cggcaggaag       240\r\n     gcgcaccccc ccagcaatcc gcgcgccggg acagaatgcc ctgcaggaac ttcttctgga       300\r\n     agaccttctc ctcctgcaaa taaaacctca cccatgaatg ctcacgcaag tttaattaca       360\r\n     gacctgaa                                                                368\r\n//\r\n" };

        [TestMethod()]
        public void ConvertEmblFastaDnaTest()
        {
            var emblConverter = SetupMock();
            string expectedLabel = ">B000263|Human mRNA for prepro cortistatin like peptide, complete cds.";
            string expectedSequence = "ACAAGATGCCATTGTCCCCCGGCCTCCTGCTGCTGCTGCTCTCCGGGGCCACGGCCACCGCTGCCCTGCCCCTGGAGGGTGGCCCCACCGGCCGAGACAGCGAGCATATGCAGGAAGCGGCAGGAATAAGGAAAAGCAGCCTCCTGACTTTCCTCGCTTGGTGGTTTGAGTGGACCTCCCAGGCCAGTGCCGGGCCCCTCATAGGAGAGGAAGCTCGGGAGGTGGCCAGGCGGCAGGAAGGCGCACCCCCCCAGCAATCCGCGCGCCGGGACAGAATGCCCTGCAGGAACTTCTTCTGGAAGACCTTCTCCTCCTGCAAATAAAACCTCACCCATGAATGCTCACGCAAGTTTAATTACAGACCTGAA";

            Dictionary<string, string> actualLabelledSequence = emblConverter.ConvertEmblFastaDna(_testCase);

            Assert.AreEqual(1, actualLabelledSequence.Count);
            Assert.AreEqual(expectedLabel, actualLabelledSequence.First().Key);
            Assert.AreEqual(expectedSequence, actualLabelledSequence.First().Value);
        }

        [TestMethod()]
        public void ConvertEmblFastaProteinTest()
        {
            var emblConverter = SetupMock();
            string expectedLabel = ">B000263|Human mRNA for prepro cortistatin like peptide, complete cds.";
            string expectedSequence = "TRCHCPPASCCCCSPGPRPPLPCPWRVAPPAETASICRKRQE*GKAAS*LSSLGGLSGPPRPVPGPS*ERKLGRWPGGRKAHPPSNPRAGTECPAGTSSGRPSPPANKTSPMNAHASLITDL";

            Dictionary<string, string> actualLabelledSequence = emblConverter.ConvertEmblFastaProtein(_testCase);

            Assert.AreEqual(1, actualLabelledSequence.Count);
            Assert.AreEqual(expectedLabel, actualLabelledSequence.First().Key);
            Assert.AreEqual(expectedSequence, actualLabelledSequence.First().Value);

        }

        private static IEmblConverter SetupMock()
        {
            return new EmblConverter();
        }
    }
} 