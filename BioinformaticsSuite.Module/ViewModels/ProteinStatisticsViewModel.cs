using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Utility;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
     public class ProteinStatisticsViewModel : SequenceViewModel
    {
        private readonly StringBuilder displayStringBuilder = new StringBuilder();

        public ProteinStatisticsViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser,
            IEventAggregator eventAggregator) : base(sequenceFactory, sequenceParser, eventAggregator) {}

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Protein;
            bool isParsedSuccessfully = SequenceParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                var parsedSequences = SequenceParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences,
                    sequenceType);
                foreach (var protein in labelledSequences)
                {
                    Dictionary<char, int> aminoAcidCount = protein.Sequence.CountAminoAcids();

                    displayStringBuilder.AppendLine(protein.Label);
                    BuildDisplayString(aminoAcidCount, protein.Sequence.Length);
                }
                ResultBoxText = displayStringBuilder.ToString();
                displayStringBuilder.Clear();
            }
            else
            {
                RaiseInvalidInputNotification(SequenceParser.ErrorMessage);
            }
            SequenceParser.ResetSequences();

        }

        private void BuildDisplayString(Dictionary<char, int> aminoAcidCount, int proteinLength)
        {

            foreach (var aminoAcid in aminoAcidCount)
            {
                displayStringBuilder.Append(aminoAcid.Key).Append(": ").Append(aminoAcid.Value).Append(" ");
            }
            displayStringBuilder.AppendLine();

            foreach (var aminoAcid in aminoAcidCount)
            {
                decimal percentage = ((decimal)aminoAcid.Value/(decimal)proteinLength)*100;
                displayStringBuilder.Append(aminoAcid.Key).Append(": ").Append(percentage).Append(" ");
            }
            displayStringBuilder.AppendLine();
        }
    }
}
