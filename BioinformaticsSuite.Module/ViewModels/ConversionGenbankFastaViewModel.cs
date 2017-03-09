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
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ConversionGenbankFastaViewModel : SequenceViewModel
    {
        private string _title = "Genbank to FASTA DNA converter";
        private readonly IGenbankConverter _genbankConverter;

        public ConversionGenbankFastaViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IGenbankConverter genbankConverter) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            _genbankConverter = genbankConverter;
            if (genbankConverter == null) throw new ArgumentNullException(nameof(genbankConverter));
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            string genbankRecord = InputBoxText;
            Dictionary<string, string> labelledFastas = _genbankConverter.ConvertGenbankFastaDna(genbankRecord);
            List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(labelledFastas, sequenceType);
            ResultBoxText = BuildDisplayString(labelledSequences);
            SelectedTab = SelectedTab.Result;
        }
    }
}
