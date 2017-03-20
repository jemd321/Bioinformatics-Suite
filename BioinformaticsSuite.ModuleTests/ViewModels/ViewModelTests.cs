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
            Assert.Fail();
        }

        [TestMethod()]
        public void ConversionEmblTranslateViewModelTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConversionGenbankFastaViewModelTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConversionGenbankTranslateViewModelTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConversionSplitFastaViewModelTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConversionCombineFastaViewModelTest()
        {

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
            Assert.Fail();
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
            const string testCase = ">test\r\nBCMPR*BRVMEGH*P";
            const string expectedResult = "";
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