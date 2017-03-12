using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Utility;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ConversionEmblFastaViewModel : SequenceViewModel
    {
        private string _title = "EMBL to FASTA DNA converter";
        private readonly IEmblConverter _emblConverter;

        public ConversionEmblFastaViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, IEventAggregator eventAggregator,
            IEmblConverter emblConverter) : base(sequenceFactory, fastaParser, eventAggregator)
        {
            _emblConverter = emblConverter;
            if (emblConverter == null) throw new ArgumentNullException(nameof(emblConverter));
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            List<string> emblRecords = ParseEmbl(InputBoxText);
            Dictionary<string, string> labelledFastas = _emblConverter.ConvertEmblFastaDna(emblRecords);
            List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(labelledFastas, sequenceType);
            ResultBoxText = BuildDisplayString(labelledSequences);
            SelectedTab = SelectedTab.Result;
        }

        private List<string> ParseEmbl(string input)
        {
            return new List<string>();
        }
    }
}
