﻿using BioinformaticsSuite.Module.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Events;
using Microsoft.Win32;

namespace BioinformaticsSuite.ModuleTests.ViewModels
{
    [TestClass()]
    public class SequenceViewModelTests
    {
        [TestMethod()]
        public void OnClearTest()
        {
            var mockViewModel = SetUpMock();
            mockViewModel.InputBoxText = "Test";
            string expectedTextBoxInput = "";

            mockViewModel.OnClear();

            Assert.AreEqual(expectedTextBoxInput, mockViewModel.InputBoxText);
        }

        [TestMethod()]
        public void OnLoadTest()
        {
            //Not easily testable until refactored away from Win32 Dialog boxes.
            Assert.Fail();
        }

        [TestMethod()]
        public void OnSaveTest()
        {
            //Not easily testable until refactored away from Win32 Dialog boxes.
            Assert.Fail();
        }

        private MockViewModel SetUpMock()
        {
            ISequenceFactory sequenceFactory = new SequenceFactory();
            IFastaParser fastaParser = new FastaParser(new SequenceValidator());
            IEventAggregator eventAggregator = new EventAggregator();
            MockViewModel mockViewModel = new MockViewModel(sequenceFactory, fastaParser, eventAggregator);
            return mockViewModel;
        }
    }
}