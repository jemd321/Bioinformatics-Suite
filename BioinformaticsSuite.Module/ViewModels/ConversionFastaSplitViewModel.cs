using System;
using System.Collections.Generic;
using System.Linq;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ConversionFastaSplitViewModel : SequenceViewModel
    {
        private readonly IFastaManipulator _fastaManipulator;
        private string _sequenceLengthBoxText;
        private string _title = "Split FASTA sequences into mutliple sequences";

        public ConversionFastaSplitViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator,
            IFastaManipulator fastaManipulator) : base(sequenceFactory, fastaParser, sequenceValidator)
        {
            _fastaManipulator = fastaManipulator;
            if (fastaManipulator == null)
            {
                throw new ArgumentNullException(nameof(fastaManipulator));
            }
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
            if (!ValidateSequenceLengthBox(out splitLength)) return;

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

                int shortestSequenceLength = parsedSequences.Select(parsedSequence => parsedSequence.Value.Length).Min();
                if (splitLength >= shortestSequenceLength)
                {
                    const string title = "Invalid Sequence Length";
                    string message = "Desired sequence length cannot be longer than then shortest sequence entered (" +
                        shortestSequenceLength + ")";
                    RaiseSimpleNotification(title, message);
                    return;
                }

                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences,
                    SequenceType.Dna);

                var splitFastasList =
                    labelledSequences.Select(
                        labelledSequence => _fastaManipulator.SplitFasta(labelledSequence, splitLength)).ToList();

                var labelledSplitSequences = new List<LabelledSequence>();
                foreach (var splitFastas in splitFastasList)
                {
                    labelledSplitSequences = SequenceFactory.CreateLabelledSequences(splitFastas, SequenceType.Dna);
                }

                ResultBoxText = BuildDisplayString(labelledSplitSequences);
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                RaiseInvalidInputNotification(FastaParser.ErrorMessage);
            }
            FastaParser.ResetSequences();
        }

        private bool ValidateSequenceLengthBox(out int splitLength)
        {
            if (SequenceLengthBoxText == "")
            {
                RaiseSimpleNotification("No Split Length Entered", "Please set the length that your sequences should be split into");
                splitLength = 0;
                return false;
            }
            if (int.TryParse(SequenceLengthBoxText, out splitLength)) return true;
            {
                RaiseSimpleNotification("Split Length Input Error", "Desired sequence length must be an integer");
                return false;
            }
        }
    }
}