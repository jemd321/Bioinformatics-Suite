using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.ViewModels;

namespace BioinformaticsSuite.ModuleTests.ViewModels
{
    class MockViewModel : SequenceViewModel
    {
        public MockViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator)
            : base(sequenceFactory, fastaParser, sequenceValidator)
        {
        }
    }
}