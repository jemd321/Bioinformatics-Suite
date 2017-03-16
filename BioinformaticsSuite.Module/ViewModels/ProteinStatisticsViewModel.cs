using System;
using System.Collections.Generic;
using System.Text;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Utility;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ProteinStatisticsViewModel : SequenceViewModel
    {
        private readonly StringBuilder _displayStringBuilder = new StringBuilder();
        private string _title = "Protein Statistics";

        public ProteinStatisticsViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser)
            : base(sequenceFactory, fastaParser)
        {
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Protein;
            bool isParsedSuccessfully = FastaParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                var parsedSequences = FastaParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences,
                    sequenceType);
                foreach (var protein in labelledSequences)
                {
                    Dictionary<char, int> aminoAcidCount = protein.Sequence.CountAminoAcids();

                    _displayStringBuilder.AppendLine(protein.Label);
                    BuildDisplayString(aminoAcidCount, protein.Sequence.Length);
                }
                SelectedTab = SelectedTab.Result;
                ResultBoxText = _displayStringBuilder.ToString();
                _displayStringBuilder.Clear();
            }
            else
            {
                RaiseInvalidInputNotification(FastaParser.ErrorMessage);
            }
            FastaParser.ResetSequences();
        }

        private void BuildDisplayString(Dictionary<char, int> aminoAcidCount, int proteinLength)
        {
            foreach (var aminoAcid in aminoAcidCount)
            {
                _displayStringBuilder.Append(aminoAcid.Key).Append(": ").Append(aminoAcid.Value).Append(" ");
            }
            _displayStringBuilder.AppendLine();

            foreach (var aminoAcid in aminoAcidCount)
            {
                decimal percentage = Math.Round(aminoAcid.Value/(decimal) proteinLength*100, 2);
                _displayStringBuilder.Append(aminoAcid.Key).Append(": ").Append(percentage).Append("% ");
            }
            _displayStringBuilder.AppendLine();
        }
    }
}