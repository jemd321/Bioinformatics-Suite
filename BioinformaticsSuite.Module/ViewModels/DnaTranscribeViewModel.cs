using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaTranscribeViewModel : SequenceViewModel
    {
        private readonly IReadingFrameFactory readingFrameFactory;
        private string title = "DNA Transcriber";

        public DnaTranscribeViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IReadingFrameFactory readingFrameFactory) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            this.readingFrameFactory = readingFrameFactory;
            if (readingFrameFactory == null) throw new ArgumentNullException(nameof(readingFrameFactory));
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            bool isParsedSuccessfully = SequenceParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                var parsedSequences = SequenceParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences, sequenceType);
                var readingFrames = CreateReadingFrames(labelledSequences);
                ResultBoxText = BuildDisplayString(readingFrames);
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                MessageBoxResult errorMessageBox = MessageBox.Show(SequenceParser.ErrorMessage);
            }
            SequenceParser.ResetSequences();
        }

        private List<ReadingFrame> CreateReadingFrames(List<LabelledSequence> labelledSequences)
        {
            return labelledSequences.Select(labelledSequence => readingFrameFactory.GetReadingFrames(labelledSequence as Dna)).ToList();
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
                    displayStringBuilder.AppendLine(DisplayStringSplitter(frame.Value));
                }
            }
            string displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }
    }
}
