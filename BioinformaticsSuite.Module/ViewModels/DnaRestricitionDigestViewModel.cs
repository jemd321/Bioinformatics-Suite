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
using NuGet;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaRestricitionDigestViewModel : SequenceViewModel
    {
        private readonly IRestrictionDigest _restrictionDigest;
        private string _title = "Restriction Digest";
        private List<string> _comboBoxEnzymes = new List<string>();
        private string _enzymeBox1Selection;
        private string _enzymeBox2Selection;
        private string _enzymeBox3Selection;

        public DnaRestricitionDigestViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, IEventAggregator eventAggregator,
            IRestrictionDigest restrictionDigest) : base(sequenceFactory, fastaParser, eventAggregator)
        {
            this._restrictionDigest = restrictionDigest;
            if (restrictionDigest == null) throw new ArgumentNullException(nameof(restrictionDigest));
            ImportEnzymes();
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public List<string> ComboBoxEnzymes
        {
            get { return _comboBoxEnzymes; }
            set { SetProperty(ref _comboBoxEnzymes, value); }
        }

        public string EnzymeBox1Selection
        {
            get { return _enzymeBox1Selection; }
            set { SetProperty(ref _enzymeBox1Selection, value); }
        }

        public string EnzymeBox2Selection
        {
            get { return _enzymeBox2Selection; }
            set { SetProperty(ref _enzymeBox2Selection, value); }
        }
        public string EnzymeBox3Selection
        {
            get { return _enzymeBox3Selection; }
            set { SetProperty(ref _enzymeBox3Selection, value); }
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

            bool isParsedSuccessfully = FastaParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                var parsedSequences = FastaParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences, SequenceType.Dna);
                var labelledDigestFragments = _restrictionDigest.FindRestrictionDigestFragments(enzymes, labelledSequences);
                ResultBoxText = BuildDisplayString(labelledDigestFragments);
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                RaiseInvalidInputNotification(FastaParser.ErrorMessage);
            }
            FastaParser.ResetSequences();
        }

        private void ImportEnzymes()
        {
            ComboBoxEnzymes.Add("No Enzyme");
            var enzymes = Resources.Resources.Enzymes.Split('\n');
            enzymes.Where(e => e != "").Select(e => e.TrimEnd('\r')).ForEach(ComboBoxEnzymes.Add);

        }

        private List<string> CollateEnzymeSelections()
        {
            var enzymes = new List<string>();
            if (!string.IsNullOrEmpty(EnzymeBox1Selection) && EnzymeBox1Selection != "No Enzyme")
            {
                enzymes.Add(EnzymeBox1Selection.Split(' ').Last().ToUpper());
            }
            if (!string.IsNullOrEmpty(EnzymeBox2Selection) && EnzymeBox2Selection != "No Enzyme")
            {
                enzymes.Add(EnzymeBox2Selection.Split(' ').Last().ToUpper());
            }
            if (!string.IsNullOrEmpty(EnzymeBox3Selection) && EnzymeBox3Selection != "No Enzyme")
            {
                enzymes.Add(EnzymeBox3Selection.Split(' ').Last().ToUpper());
            }
            return enzymes;
        }

        // Concatenates labels and sequences for display in the sequence text box.
        private string BuildDisplayString(List<LabelledDigestFragments> labelledDisDigestFragments)
        {
            var displayStringBuilder = new StringBuilder();
            var labeldBuilder = new StringBuilder();
            foreach (var labelledDigest in labelledDisDigestFragments)
            {
                displayStringBuilder.AppendLine(labelledDigest.Label).AppendLine();
                var orderFragments = labelledDigest.DigestFramgments.OrderByDescending(n => n.Fragment.Length);
                foreach (var fragment in orderFragments)
                {
                    labeldBuilder.Append(">Fragment: ")
                        .Append(fragment.CutPosition)
                        .Append("-")
                        .Append(fragment.CutPosition + fragment.Fragment.Length)
                        .Append(" Enzyme: ")
                        .Append(fragment.Enzyme)
                        .Append(" Length: ")
                        .Append(fragment.Fragment.Length);
                    displayStringBuilder.AppendLine(labeldBuilder.ToString());
                    displayStringBuilder.AppendLine(fragment.Fragment);
                    labeldBuilder.Clear();
                }
            }
            string displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }
    }
}
