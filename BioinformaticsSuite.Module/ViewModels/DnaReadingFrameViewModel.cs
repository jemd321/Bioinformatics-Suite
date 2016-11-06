using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaReadingFrameViewModel : SequenceViewModel
    {        
        private readonly IReadingFrameFactory readingFrameFactory;
        private string title = "Find Reading Frames";
        private double infoBarHeight;

        public DnaReadingFrameViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IReadingFrameFactory readingFrameFactory) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            this.readingFrameFactory = readingFrameFactory;
            if(readingFrameFactory == null) throw new ArgumentNullException(nameof(readingFrameFactory));

            openconnections = new DelegateCommand(RaiseConnctionsOptionsRequest);
            this.OptionSettingConformationRequest = new InteractionRequest<IConfirmation>();
        }

        public InteractionRequest<IConfirmation> OptionSettingConformationRequest { get; private set; }


        private readonly ICommand openconnections;
        public ICommand OpenConnections { get { return openconnections; } }

        private void RaiseConnctionsOptionsRequest()
        {
            this.OptionSettingConformationRequest.Raise(new Confirmation { Title = "helloeold"}, OnConnectionOptionsResponse);
        }

        protected virtual void OnConnectionOptionsResponse(IConfirmation context)
        {
            
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public double InfoBarHeight
        {
            get { return infoBarHeight; }
            set { SetProperty(ref infoBarHeight, value); }
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
