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

        public ProteinOpenReadingFrameViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser,
            IOpenReadingFrameFinder openReadingFrameFinder)
            : base(sequenceFactory, fastaParser)
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
            const SequenceType sequenceType = SequenceType.Dna;
            var isParsedSuccesfully = FastaParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccesfully)
            {
                var parsedSequences = FastaParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences,
                    sequenceType);
                foreach (var labelledSequence in labelledSequences)
                {
                    Dictionary<string, string> labelledOrfs =
                        _openReadingFrameFinder.FindOpenReadingFrames((Dna) labelledSequence);
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
            FastaParser.ResetSequences();
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