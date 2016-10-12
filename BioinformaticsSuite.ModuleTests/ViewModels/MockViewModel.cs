using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Events;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.ViewModels;
using Prism.Commands;
using Prism.Events;

namespace BioinformaticsSuite.ModuleTests.ViewModels
{
    class MockViewModel : SequenceViewModel
    {
        public MockViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser,
            IEventAggregator eventAggregator) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
        }      

        public override void SubscribeToEvents()
        {
            EventAggregator.GetEvent<WindowSizeChanged>().Subscribe(ResizeTextBox);
        }      
    }
}
