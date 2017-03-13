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
        private readonly IGenbankParser _genbankParser;

        public ConversionGenbankFastaViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, IEventAggregator eventAggregator,
            IGenbankConverter genbankConverter, IGenbankParser genbankParser) : base(sequenceFactory, fastaParser, eventAggregator)
        {
            _genbankConverter = genbankConverter;
            _genbankParser = genbankParser;
            if (genbankConverter == null) throw new ArgumentNullException(nameof(genbankConverter));
            if (genbankParser == null) throw new ArgumentNullException(nameof(genbankParser));
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            if (_genbankParser.TryParseGenbankFile(InputBoxText))
            {
                var genbankRecords = _genbankParser.GenbankRecords;
                Dictionary<string, string> labelledFastas = _genbankConverter.ConvertGenbankFastaDna(genbankRecords);
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(labelledFastas, sequenceType);
                ResultBoxText = BuildDisplayString(labelledSequences);
            }
            else
            {
                RaiseInvalidInputNotification(_genbankParser.ErrorMessage);
                _genbankParser.ResetSequences();
                return;
            }
            SelectedTab = SelectedTab.Result;
            _genbankParser.ResetSequences();
        }
    }
}
