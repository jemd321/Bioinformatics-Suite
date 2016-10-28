using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Views.PopupViews;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaFindMotifViewModel : SequenceViewModel
    {
        private readonly IMotifFinder motifFinder;
        private string title = "Find Motifs";
        private string motifBoxText;

        public DnaFindMotifViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IMotifFinder motifFinder) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            this.motifFinder = motifFinder;
            if (motifFinder == null) throw new ArgumentNullException(nameof(motifFinder));

            HelpPopupViewRequest = new InteractionRequest<INotification>();
            RaiseHelpPopupCommand = new DelegateCommand(RaiseHelpPopupView);

        }

        public InteractionRequest<INotification> HelpPopupViewRequest { get; private set; }
        public ICommand RaiseHelpPopupCommand { get; private set; }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public string MotifBoxText
        {
            get { return motifBoxText; }
            set { SetProperty(ref motifBoxText, value); }
        }

        private void RaiseHelpPopupView()
        {
            HelpPopupViewRequest.Raise(new Notification() {Title = "", Content = ""});
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            bool isParsedSuccessfully = SequenceParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                string motif = MotifBoxText;
                var parsedSequences = SequenceParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences, sequenceType);
                Dictionary<string, MatchCollection> labelledMatches = motifFinder.FindMotif(motif, labelledSequences);
                ResultBoxText = BuildDisplayString(labelledMatches);
                SelectedTab = SelectedTab.Result;               
            }
            else
            {
                RaiseSequenceValidationErrorNotification(SequenceParser.ErrorMessage);
            }
            SequenceParser.ResetSequences();
        }

        // Concatenates labels and sequences for display in the sequence text box.
        private string BuildDisplayString(Dictionary<string, MatchCollection> labelledMatches)
        {
            var displayStringBuilder = new StringBuilder();
            foreach (var labelledMatch in labelledMatches)
            {
                string label = labelledMatch.Key;
                displayStringBuilder.AppendLine(label);

                var matches = labelledMatch.Value;
                foreach (Match match in matches)
                {
                    var startIndex = match.Index;
                    var endIndex = startIndex + match.Length;
                    displayStringBuilder.Append(startIndex);
                    displayStringBuilder.Append(" - ");
                    displayStringBuilder.Append(endIndex);
                    displayStringBuilder.Append("    ");
                }
                displayStringBuilder.AppendLine();
            }

            var displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }
    }
}
