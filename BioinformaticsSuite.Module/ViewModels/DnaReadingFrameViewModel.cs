using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaReadingFrameViewModel : SequenceViewModel
    {
        private readonly IReadingFrameFactory _readingFrameFactory;
        private string _title = "Find Reading Frames";

        public DnaReadingFrameViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator,
            IReadingFrameFactory readingFrameFactory) : base(sequenceFactory, fastaParser, sequenceValidator)
        {
            _readingFrameFactory = readingFrameFactory;
            if (readingFrameFactory == null) throw new ArgumentNullException(nameof(readingFrameFactory));
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
                bool isParsedSuccessfully = FastaParser.TryParseInput(InputBoxText);
                if (isParsedSuccessfully)
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
                    var readingFrames = CreateReadingFrames(labelledSequences);
                    ResultBoxText = BuildDisplayString(readingFrames);
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

        private List<ReadingFrame> CreateReadingFrames(List<LabelledSequence> labelledSequences)
        {
            return
                labelledSequences.Select(
                    labelledSequence => _readingFrameFactory.GetReadingFrames(labelledSequence as Dna)).ToList();
        }

        // Concatenates labels and sequences for display in the sequence text box.
        private string BuildDisplayString(List<ReadingFrame> readingFrames)
        {
            StringBuilder displayStringBuilder = new StringBuilder();
            foreach (ReadingFrame frames in readingFrames)
            {
                foreach (KeyValuePair<string, string> frame in frames.LabelledFrames)
                {
                    displayStringBuilder.AppendLine(frame.Key);
                    //displayStringBuilder.AppendLine(DisplayStringSplitter(frame.Value));
                    displayStringBuilder.AppendLine(frame.Value);
                }
            }
            string displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }
    }
}