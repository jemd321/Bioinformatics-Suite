using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaRestricitionDigestViewModel : SequenceViewModel
    {
        private readonly IReadingFrameFactory readingFrameFactory;
        private string title = "Restriction Digest";
        private List<string> comboBoxEnzymes = new List<string>();
        private string enzymeBox1Selection;
        private string enzymeBox2Selection;
        private string enzymeBox3Selection;

        public DnaRestricitionDigestViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IReadingFrameFactory readingFrameFactory) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            this.readingFrameFactory = readingFrameFactory;
            if (readingFrameFactory == null) throw new ArgumentNullException(nameof(readingFrameFactory));
            ImportEnzymes();
        }

        public ICommand UpdateCombobox;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public List<string> ComboBoxEnzymes
        {
            get { return comboBoxEnzymes; }
            set { SetProperty(ref comboBoxEnzymes, value); }
        }

        public string EnzymeBox1Selection
        {
            get { return enzymeBox1Selection; }
            set { SetProperty(ref enzymeBox1Selection, value); }
        }

        public string EnzymeBox2Selection
        {
            get { return enzymeBox1Selection; }
            set { SetProperty(ref enzymeBox2Selection, value); }
        }
        public string EnzymeBox3Selection
        {
            get { return enzymeBox1Selection; }
            set { SetProperty(ref enzymeBox3Selection, value); }
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

        private void ImportEnzymes()
        {
            using (var importer = new StreamReader("Enzymes.txt"))
            {
                while (!importer.EndOfStream)
                {
                    string enzyme = importer.ReadLine();
                    comboBoxEnzymes.Add(enzyme);
                }
            }
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
