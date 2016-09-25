using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Microsoft.Win32;
using NuGet;
using Prism.Commands;
using Prism.Mvvm;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaReadingFrameViewModel : SequenceViewModel
    {
        private readonly IReadingFrameFactory readingFrameFactory;

        public DnaReadingFrameViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser,
            IReadingFrameFactory readingFrameFactory) : base(sequenceFactory, sequenceParser)
        {
            this.readingFrameFactory = readingFrameFactory;
            if(readingFrameFactory == null) throw new ArgumentNullException(nameof(readingFrameFactory));
        }

        public override void OnSubmit()
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
            var readingFrames = new List<ReadingFrame>();
            foreach (var labelledSequence in labelledSequences)
            {
                readingFrames.Add(readingFrameFactory.GetReadingFrames(labelledSequence as Dna));
            }
            return readingFrames;
        }

        private string BuildDisplayString(List<ReadingFrame> readingFrames)
        {
            StringBuilder displayStringBuilder = new StringBuilder();
            foreach (ReadingFrame frames in readingFrames)
            {
                foreach (KeyValuePair<string, string> frame in frames.LabelledFrames)
                {
                    displayStringBuilder.Append(frame.Key);
                    displayStringBuilder.AppendLine();
                    displayStringBuilder.Append(frame.Value);
                    displayStringBuilder.AppendLine();
                }
            }
            string displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }


    }
}
