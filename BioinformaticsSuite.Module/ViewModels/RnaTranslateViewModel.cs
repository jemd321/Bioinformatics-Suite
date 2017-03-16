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
    public class RnaTranslateViewModel : SequenceViewModel
    {
        private string _title = "Translate RNA";

        public RnaTranslateViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser) : base(sequenceFactory, fastaParser) { }

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
                var translatedSequences = new Dictionary<string, string>();
                foreach (var labelledSequence in parsedSequences)
                {
                    string rnaSequence = labelledSequence.Value;
                    string proteinSequence = Translation.TranslateRnaToProtein(rnaSequence);
                    translatedSequences.Add(labelledSequence.Key, proteinSequence);
                }
                List<LabelledSequence> labelledProteins = SequenceFactory.CreateLabelledSequences(translatedSequences, SequenceType.Protein);
                ResultBoxText = BuildDisplayString(labelledProteins);
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
