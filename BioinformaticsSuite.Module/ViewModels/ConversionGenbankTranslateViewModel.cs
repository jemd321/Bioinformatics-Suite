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
    public class ConversionGenbankTranslateViewModel : SequenceViewModel
    {
        private string _title = "Genbank Translate to protein FASTA";

        private readonly IGenbankConverter _genbankConverter;
        private readonly IGenbankParser _genbankParser;

        public ConversionGenbankTranslateViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, IEventAggregator eventAggregator,
            IGenbankConverter genbankConverter, IGenbankParser genbankParser) : base(sequenceFactory, fastaParser, eventAggregator)
        {
            _genbankConverter = genbankConverter;
            _genbankParser = genbankParser;
            if (_genbankConverter == null) throw new ArgumentNullException(nameof(genbankConverter));
            if (_genbankParser == null) throw new ArgumentNullException(nameof(genbankParser));
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
                Dictionary<string, string> labelledFastas = _genbankConverter.ConvertGenbankFastaProtein(genbankRecords);
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
