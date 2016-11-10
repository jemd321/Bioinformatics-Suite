using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ProteinOpenReadingFrameViewModel : SequenceViewModel
    {
        private string title = "Open Reading Frames";
        private readonly StringBuilder displayStringBuilder = new StringBuilder();
        private readonly IOpenReadingFrameFinder openReadingFrameFinder;

        public ProteinOpenReadingFrameViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator, IOpenReadingFrameFinder openReadingFrameFinder) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            this.openReadingFrameFinder = openReadingFrameFinder;
            if(this.openReadingFrameFinder == null) throw new ArgumentNullException(nameof(openReadingFrameFinder));
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            var isParsedSuccesfully = SequenceParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccesfully)
            {
                var parsedSequences = SequenceParser.ParsedSequences;
                bool containsOrfs = false;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences, sequenceType);
                foreach (var labelledSequence in labelledSequences)
                {
                    Dictionary<string, string> labelledOrfs = openReadingFrameFinder.FindOpenReadingFrames((Dna) labelledSequence);
                    BuildDisplayString(labelledSequence.Label, labelledOrfs);
                }
                ResultBoxText = displayStringBuilder.ToString();
                displayStringBuilder.Clear();
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                RaiseInvalidInputNotification(SequenceParser.ErrorMessage);
            }
            SequenceParser.ResetSequences();
        }

        private void BuildDisplayString(string label, Dictionary<string, string> labelledOrfs)
        {
            displayStringBuilder.AppendLine(label);
            foreach (var orf in labelledOrfs)
            {
                displayStringBuilder.AppendLine(orf.Key);
                displayStringBuilder.AppendLine(orf.Value);
            }
        }
    }
}
