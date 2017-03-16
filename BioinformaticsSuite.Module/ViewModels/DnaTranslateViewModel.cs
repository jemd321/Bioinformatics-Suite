using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Utility;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaTranslateViewModel : SequenceViewModel
    {
        private string _title = "Translate DNA";

        public DnaTranslateViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser) : base(sequenceFactory, fastaParser) { }

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
                var parsedSequences = FastaParser.ParsedSequences;
                var translatedSequences = new Dictionary<string, string>();
                foreach (var labelledSequence in parsedSequences)
                {
                    string dnaSequence = labelledSequence.Value;
                    string proteinSequence = Translation.TranslateDnaToProtein(dnaSequence);
                    translatedSequences.Add(labelledSequence.Key, proteinSequence);
                }
                List<LabelledSequence> labelledProteins  = SequenceFactory.CreateLabelledSequences(translatedSequences, SequenceType.Protein);
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
