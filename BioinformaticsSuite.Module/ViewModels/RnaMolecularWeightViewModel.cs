using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class RnaMolecularWeightViewModel : SequenceViewModel
    {
        private readonly IMolecularWeightCalculator _molecularWeightCalculator;
        private string _title = "RNA Molecular Weight";

        public RnaMolecularWeightViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser,
            IMolecularWeightCalculator molecularWeightCalculator) : base(sequenceFactory, fastaParser)
        {
            _molecularWeightCalculator = molecularWeightCalculator;
            if (molecularWeightCalculator == null) throw new ArgumentNullException(nameof(molecularWeightCalculator));
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Rna;
            bool isParsedSuccessfully = FastaParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                var parsedSequences = FastaParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences,
                    sequenceType);
                foreach (var labelledSequence in labelledSequences)
                {
                    _molecularWeightCalculator.CalculateMolecularWeight(labelledSequence);
                }
                ResultBoxText = BuildDisplayString(labelledSequences);
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                RaiseInvalidInputNotification(FastaParser.ErrorMessage);
            }
            FastaParser.ResetSequences();
        }

        // Concatenates labels and sequences for display in the sequence text box.
        public override string BuildDisplayString(List<LabelledSequence> labelledSequences)
        {
            StringBuilder displayStringBuilder = new StringBuilder();
            foreach (var labelledSequence in labelledSequences)
            {
                displayStringBuilder.AppendLine(labelledSequence.Label);
                displayStringBuilder.Append(labelledSequence.MolecularWeight.ToString(CultureInfo.InvariantCulture));
                displayStringBuilder.AppendLine(" kDa");
            }
            var displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }
    }
}