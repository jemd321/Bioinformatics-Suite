using System;
using System.Collections.Generic;
using System.Text;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ProteinOpenReadingFrameViewModel : SequenceViewModel
    {
        private readonly StringBuilder _displayStringBuilder = new StringBuilder();
        private readonly IOpenReadingFrameFinder _openReadingFrameFinder;
        private string _title = "Open Reading Frames";

        public ProteinOpenReadingFrameViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator,
            IOpenReadingFrameFinder openReadingFrameFinder)
            : base(sequenceFactory, fastaParser, sequenceValidator)
        {
            _openReadingFrameFinder = openReadingFrameFinder;
            if (_openReadingFrameFinder == null) throw new ArgumentNullException(nameof(openReadingFrameFinder));
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public override void OnRun()
        {
            try
            {
                const SequenceType sequenceType = SequenceType.Dna;
                var isParsedSuccesfully = FastaParser.TryParseInput(InputBoxText);
                if (isParsedSuccesfully)
                {
                    var parsedSequences = FastaParser.ParsedSequences;
                    if (ValidateSequences)
                    {
                        bool isValid = SequenceValidator.TryValidateSequence(parsedSequences, sequenceType);
                        if (!isValid)
                        {
                            RaiseInvalidInputNotification(SequenceValidator.ErrorMessage);
                            return;
                        }
                    }
                    List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences,
                        sequenceType);
                    foreach (var labelledSequence in labelledSequences)
                    {
                        Dictionary<string, string> labelledOrfs =
                            _openReadingFrameFinder.FindOpenReadingFrames((Dna)labelledSequence);
                        BuildDisplayString(labelledSequence.Label, labelledOrfs);
                    }
                    ResultBoxText = _displayStringBuilder.ToString();
                    _displayStringBuilder.Clear();
                    SelectedTab = SelectedTab.Result;
                }
                else
                {
                    RaiseInvalidInputNotification(FastaParser.ErrorMessage);
                }
            }
            finally
            {
                FastaParser.ResetSequences();
            }            
        }

        private void BuildDisplayString(string label, Dictionary<string, string> labelledOrfs)
        {
            foreach (var orf in labelledOrfs)
            {
                _displayStringBuilder.AppendLine(orf.Key);
                _displayStringBuilder.AppendLine(orf.Value);
            }
        }
    }
}