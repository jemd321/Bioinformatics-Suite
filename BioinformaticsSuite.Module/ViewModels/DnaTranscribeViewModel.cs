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
    public class DnaTranscribeViewModel : SequenceViewModel
    {
        private string _title = "DNA Transcriber";

        public DnaTranscribeViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser) : base(sequenceFactory, fastaParser)
        {
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
                var parsedSequences = FastaParser.ParsedSequences;
                var transcribedSequences = new Dictionary<string, string>();
                foreach (var labelledSequence in parsedSequences)
                {
                    string dnaSequence = labelledSequence.Value;
                    string proteinSequence = Translation.TranscribeDnaToRna(dnaSequence);
                    transcribedSequences.Add(labelledSequence.Key, proteinSequence);
                }
                List<LabelledSequence> labelledRna = SequenceFactory.CreateLabelledSequences(transcribedSequences, SequenceType.Rna);
                ResultBoxText = BuildDisplayString(labelledRna);
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
