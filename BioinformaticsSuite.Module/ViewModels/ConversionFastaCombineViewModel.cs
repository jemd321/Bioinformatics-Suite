using System;
using System.Collections.Generic;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ConversionFastaCombineViewModel : SequenceViewModel
    {
        private readonly IFastaManipulator _fastaManipulator;
        private string _title = "Combine multiple FASTA sequences into a single sequence";

        public ConversionFastaCombineViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator,
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

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            bool isParsedSuccessfully = FastaParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                Dictionary<string, string> parsedSequences = FastaParser.ParsedSequences;
                List<LabelledSequence> labelledFastas = SequenceFactory.CreateLabelledSequences(parsedSequences,
                    SequenceType.Dna);
                var combinedFastas =
                    SequenceFactory.CreateLabelledSequences(_fastaManipulator.CombineFasta(labelledFastas),
                        SequenceType.Dna);
                ResultBoxText = BuildDisplayString(combinedFastas);
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                RaiseInvalidInputNotification(FastaParser.ErrorMessage);
            }
            FastaParser.ResetSequences();
        }
    }
}