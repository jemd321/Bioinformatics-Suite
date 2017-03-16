using System;
using System.Collections.Generic;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ConversionGenbankTranslateViewModel : SequenceViewModel
    {
        private readonly IGenbankConverter _genbankConverter;
        private readonly IGenbankParser _genbankParser;
        private string _title = "Genbank Translate to protein FASTA";

        public ConversionGenbankTranslateViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser,
            IGenbankConverter genbankConverter, IGenbankParser genbankParser) : base(sequenceFactory, fastaParser)
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
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(labelledFastas,
                    sequenceType);
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