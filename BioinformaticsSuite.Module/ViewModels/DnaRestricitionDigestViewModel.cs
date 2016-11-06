using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaRestricitionDigestViewModel : SequenceViewModel
    {
        private readonly IRestrictionDigest restrictionDigest;
        private string title = "Restriction Digest";
        private List<string> comboBoxEnzymes = new List<string>();
        private string enzymeBox1Selection;
        private string enzymeBox2Selection;
        private string enzymeBox3Selection;

        public DnaRestricitionDigestViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IRestrictionDigest restrictionDigest) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            this.restrictionDigest = restrictionDigest;
            if (restrictionDigest == null) throw new ArgumentNullException(nameof(restrictionDigest));
            ImportEnzymes();
        }

        public ICommand UpdateCombobox;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public List<string> ComboBoxEnzymes
        {
            get { return comboBoxEnzymes; }
            set { SetProperty(ref comboBoxEnzymes, value); }
        }

        public string EnzymeBox1Selection
        {
            get { return enzymeBox1Selection; }
            set { SetProperty(ref enzymeBox1Selection, value); }
        }

        public string EnzymeBox2Selection
        {
            get { return enzymeBox2Selection; }
            set { SetProperty(ref enzymeBox2Selection, value); }
        }
        public string EnzymeBox3Selection
        {
            get { return enzymeBox3Selection; }
            set { SetProperty(ref enzymeBox3Selection, value); }
        }
        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            var enzymes = CollateEnzymeSelections();

            if (enzymes.Count == 0)
            {
                RaiseInvalidInputNotification("Please select at least one Restriction Enzyme to digest with.");
                return;
            }

            bool isParsedSuccessfully = SequenceParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                var parsedSequences = SequenceParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences, SequenceType.Dna);
                var labelledDigestFragments = restrictionDigest.FindRestrictionDigestFragments(enzymes, labelledSequences);
                string displayString = BuildDisplayString(labelledDigestFragments);
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                RaiseInvalidInputNotification(SequenceParser.ErrorMessage);
            }
            SequenceParser.ResetSequences();
        }

        private void ImportEnzymes()
        {
            using (var importer = new StreamReader("Enzymes.txt"))
            {
                while (!importer.EndOfStream)
                {
                    string enzyme = importer.ReadLine();
                    comboBoxEnzymes.Add(enzyme);
                }
            }
        }

        private List<string> CollateEnzymeSelections()
        {
            var enzymes = new List<string>();
            if (!string.IsNullOrEmpty(EnzymeBox1Selection))
            {
                enzymes.Add(EnzymeBox1Selection);
            }
            if (!string.IsNullOrEmpty(EnzymeBox2Selection))
            {
                enzymes.Add(EnzymeBox2Selection);
            }
            if (!string.IsNullOrEmpty(EnzymeBox3Selection))
            {
                enzymes.Add(EnzymeBox3Selection);
            }
            return enzymes;
        }

        // Concatenates labels and sequences for display in the sequence text box.
        private string BuildDisplayString(List<LabelledDigestFragments> labelledDisDigestFragments)
        {
            var displayStringBuilder = new StringBuilder();
            foreach (var labelledDigest in labelledDisDigestFragments)
            {
                displayStringBuilder.AppendLine(labelledDigest.Label);
                var orderFragments = labelledDigest.DigestFramgments.OrderBy(n => n.Enzyme.Length);
                foreach (var fragment in orderFragments)
                {
                    displayStringBuilder.AppendLine(fragment.Fragment);
                }
            }
            string displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }
    }
}
