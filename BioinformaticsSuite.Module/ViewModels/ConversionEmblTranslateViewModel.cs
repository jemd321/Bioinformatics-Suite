using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Utility;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ConversionEmblTranslateViewModel : SequenceViewModel
    {
        private string _title = "Convert EMBL format to a protein sequence in FASTA";
        private static readonly Regex FileSeparatorRegex = new Regex(@"(?<!http:)\/\/", RegexOptions.Compiled);

        public ConversionEmblTranslateViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, IEventAggregator eventAggregator,
            IReadingFrameFactory readingFrameFactory) : base(sequenceFactory, fastaParser, eventAggregator)
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
                var translatedSequences = new Dictionary<string, string>();
                foreach (var labelledSequence in parsedSequences)
                {
                    string dnaSequence = labelledSequence.Value;
                    string proteinSequence = Translation.TranslateDnaToProtein(dnaSequence);
                    translatedSequences.Add(labelledSequence.Key, proteinSequence);
                }
                List<LabelledSequence> labelledProteins = SequenceFactory.CreateLabelledSequences(translatedSequences, SequenceType.Protein);
                ResultBoxText = BuildDisplayString(labelledProteins);
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                MessageBoxResult errorMessageBox = MessageBox.Show(FastaParser.ErrorMessage);
            }
            FastaParser.ResetSequences();
        }
    }
}
