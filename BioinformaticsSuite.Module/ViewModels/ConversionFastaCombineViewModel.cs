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
    public class ConversionFastaCombineViewModel : SequenceViewModel
    {
        private string _title = "Combine multiple FASTA sequences into a single sequence";
        private readonly IFastaManipulator _fastaManipulator;

        public ConversionFastaCombineViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser,
            IFastaManipulator fastaManipulator) : base(sequenceFactory, fastaParser)
        {
            _fastaManipulator = fastaManipulator;
            if(fastaManipulator == null) { throw new ArgumentNullException(nameof(fastaManipulator));}
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
                List<LabelledSequence> labelledFastas = SequenceFactory.CreateLabelledSequences(parsedSequences, SequenceType.Dna);
                var combinedFastas = SequenceFactory.CreateLabelledSequences(_fastaManipulator.CombineFasta(labelledFastas), SequenceType.Dna);
                ResultBoxText = BuildDisplayString(combinedFastas);
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
