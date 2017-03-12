using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ConversionFastaSplitViewModel : SequenceViewModel
    {
        private string _title = "Split FASTA sequences into mutliple sequences";
        private string _sequenceLengthBoxText;
        private readonly IFastaManipulator _fastaManipulator;

        public ConversionFastaSplitViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IFastaManipulator fastaManipulator) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            _fastaManipulator = fastaManipulator;
            if (fastaManipulator == null) { throw new ArgumentNullException(nameof(fastaManipulator)); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string SequenceLengthBoxText
        {
            get { return _sequenceLengthBoxText; }
            set { SetProperty(ref _sequenceLengthBoxText, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            int splitLength;
            if(!ValidateSequenceLengthBox(out splitLength)) return;
            bool isParsedSuccessfully = SequenceParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                Dictionary<string, string> parsedSequences = SequenceParser.ParsedSequences;
                int shortestSequenceLength = parsedSequences.Select(parsedSequence => parsedSequence.Value.Length).Min();
                if (splitLength >= shortestSequenceLength)
                {
                    MessageBoxResult sequenceLengthErrorMessageBox =
                        MessageBox.Show("Desired sequence length cannot be longer than then shortest sequence entered (" + shortestSequenceLength + ")");
                    return;
                }


                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences, SequenceType.Dna);
                var splitFastasList = labelledSequences.Select(labelledSequence => _fastaManipulator.SplitFasta(labelledSequence, splitLength)).ToList();
                foreach (var splitFastas in splitFastasList)
                {
                    SequenceFactory.CreateLabelledSequences(splitFastas, SequenceType.Dna);
                }

                ResultBoxText = BuildDisplayString(labelledSequences);
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                MessageBoxResult errorMessageBox = MessageBox.Show(SequenceParser.ErrorMessage);
            }
            SequenceParser.ResetSequences();
        }

        private bool ValidateSequenceLengthBox(out int splitLength)
        {
            if (SequenceLengthBoxText == "")
            {
                MessageBoxResult sequenceLengthErrorMessageBox =
                    MessageBox.Show("Please set the length that your sequences should be split into");
                splitLength = 0;
                return false;
            }
            if (int.TryParse(SequenceLengthBoxText, out splitLength)) return true;
            {
                MessageBoxResult sequenceLengthErrorMessageBox =
                    MessageBox.Show("Desired sequence length must be an integer");
                return false;
            }
        }
    }
}
