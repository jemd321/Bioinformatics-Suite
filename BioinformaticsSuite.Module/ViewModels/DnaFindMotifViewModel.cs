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
        private readonly IMotifFinder _motifFinder;
        private string _title = "Find Motifs";
        private string _motifBoxText;

        public DnaFindMotifViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IMotifFinder motifFinder) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            this._motifFinder = motifFinder;
            if (motifFinder == null) throw new ArgumentNullException(nameof(motifFinder));

            HelpPopupViewRequest = new InteractionRequest<INotification>();
            RaiseHelpPopupCommand = new DelegateCommand(RaiseHelpPopupView);

        }

        public InteractionRequest<INotification> HelpPopupViewRequest { get; private set; }
        public ICommand RaiseHelpPopupCommand { get; private set; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string MotifBoxText
        {
            get { return _motifBoxText; }
            set { SetProperty(ref _motifBoxText, value); }
        }

        private void RaiseHelpPopupView()
        {
            HelpPopupViewRequest.Raise(new Notification() {Title = "", Content = ""});
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            string motif = MotifBoxText.ToUpper();
            if (string.IsNullOrWhiteSpace(motif))
            {
                RaiseInvalidInputNotification("Please enter a valid motif in the box below.");
                return;
            }

            string parsedMotif;
            bool isValidMotif = _motifFinder.TryParseMotif(motif, sequenceType, out parsedMotif);
            if (isValidMotif)
            {
                bool isParsedSuccessfully = SequenceParser.TryParseInput(InputBoxText, sequenceType);
                if (isParsedSuccessfully)
                {
                    var parsedSequences = SequenceParser.ParsedSequences;
                    List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences, sequenceType);

                    var labelledMatches = new Dictionary<string, MatchCollection>();
                    foreach (var labelledSequence in labelledSequences)
                    {
                        var matches = _motifFinder.FindMotif(parsedMotif, labelledSequence);
                        labelledMatches.Add(labelledSequence.Label, matches);
                    }
                    ResultBoxText = BuildDisplayString(labelledMatches);
                    SelectedTab = SelectedTab.Result;
                }
                else
                {
                    RaiseInvalidInputNotification(SequenceParser.ErrorMessage);
                }
            }
            else
            {
                RaiseInvalidInputNotification("A DNA motif may only contain IUPAC base codes, click the 'Help/IUPAC Codes' button for more information \n\n" + _motifFinder.InvalidMotifMessage);
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

                if (labelledMatch.Value == null)
                {
                    displayStringBuilder.AppendLine("Motif Not Found");
                }
                else
                {
                    var matches = labelledMatch.Value;
                    foreach (Match match in matches)
                    {
                        var startIndex = match.Index + 1;
                        var endIndex = startIndex + match.Length - 1;
                        displayStringBuilder.Append(startIndex);
                        displayStringBuilder.Append(" - ");
                        displayStringBuilder.Append(endIndex);
                        displayStringBuilder.Append("    ");
                    }
                    displayStringBuilder.AppendLine();
                }
            }

            var displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }
    }
}
