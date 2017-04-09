using System;
using System.Collections.Generic;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ConversionGenbankFastaViewModel : SequenceViewModel
    {
        private readonly IGenbankConverter _genbankConverter;
        private readonly IGenbankParser _genbankParser;
        private string _title = "Genbank to FASTA DNA converter";

        public ConversionGenbankFastaViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator,
            IGenbankConverter genbankConverter, IGenbankParser genbankParser) : base(sequenceFactory, fastaParser, sequenceValidator)
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
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(labelledFastas,
                    sequenceType);
                ResultBoxText = BuildDisplayString(labelledSequences);
            }
            else
            {
                RaiseSimpleNotification("Invalid Genbank Record", _genbankParser.ErrorMessage);
                _genbankParser.ResetSequences();
                return;
            }
            SelectedTab = SelectedTab.Result;
            _genbankParser.ResetSequences();
        }
    }
}