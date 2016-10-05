using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Microsoft.Win32;
using NuGet;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaReadingFrameViewModel : SequenceViewModel
    {        
        private readonly IReadingFrameFactory readingFrameFactory;
        private string title = "Find Reading Frames";

        public DnaReadingFrameViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IReadingFrameFactory readingFrameFactory) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            this.readingFrameFactory = readingFrameFactory;
            if(readingFrameFactory == null) throw new ArgumentNullException(nameof(readingFrameFactory));
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;

            bool isParsedSuccessfully = SequenceParser.TryParseInput(TextBoxInput, sequenceType);
            if (isParsedSuccessfully)
            {
                var parsedSequences = SequenceParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences, sequenceType);
                var readingFrames = CreateReadingFrames(labelledSequences);
                TextBoxInput = BuildDisplayString(readingFrames);
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
