﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Microsoft.Practices.ObjectBuilder2;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaRestricitionDigestViewModel : SequenceViewModel
    {
        private readonly IRestrictionDigest _restrictionDigest;
        private List<string> _comboBoxEnzymes = new List<string>();
        private string _enzymeBox1Selection;
        private string _enzymeBox2Selection;
        private string _enzymeBox3Selection;
        private string _title = "Restriction Digest";

        public DnaRestricitionDigestViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator,
            IRestrictionDigest restrictionDigest) : base(sequenceFactory, fastaParser, sequenceValidator)
        {
            _restrictionDigest = restrictionDigest;
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
            try
            {
                const SequenceType sequenceType = SequenceType.Dna;
                var enzymes = CollateEnzymeSelections();

                if (enzymes.Count == 0)
                {
                    RaiseSimpleNotification("No Enzymes Selected", "Please select at least one Restriction Enzyme to digest with.");
                    return;
                }

                bool isParsedSuccessfully = FastaParser.TryParseInput(InputBoxText);
                if (isParsedSuccessfully)
                {
                    var parsedSequences = FastaParser.ParsedSequences;
                    if (ValidateSequences)
                    {
                        bool isValid = SequenceValidator.TryValidateSequence(parsedSequences, sequenceType);
                        if (!isValid)
                        {
                            RaiseInvalidInputNotification(SequenceValidator.ErrorMessage);
                            return;
                        }
                    }
                    List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences,
                        SequenceType.Dna);
                    var labelledDigestFragments = _restrictionDigest.FindRestrictionDigestFragments(enzymes,
                        labelledSequences);
                    ResultBoxText = BuildDisplayString(labelledDigestFragments);
                    SelectedTab = SelectedTab.Result;
                }
                else
                {
                    RaiseInvalidInputNotification(FastaParser.ErrorMessage);
                }
            }
            finally
            {
                FastaParser.ResetSequences();
            }          
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
            if (!string.IsNullOrEmpty(EnzymeBox1Selection) && (EnzymeBox1Selection != "No Enzyme"))
            {
                enzymes.Add(EnzymeBox1Selection.Split(' ').Last().ToUpper());
            }
            if (!string.IsNullOrEmpty(EnzymeBox2Selection) && (EnzymeBox2Selection != "No Enzyme"))
            {
                enzymes.Add(EnzymeBox2Selection.Split(' ').Last().ToUpper());
            }
            if (!string.IsNullOrEmpty(EnzymeBox3Selection) && (EnzymeBox3Selection != "No Enzyme"))
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
                string label = labelledDigest.Label;
                var orderedFragments = labelledDigest.DigestFramgments.OrderByDescending(n => n.Fragment.Length);
                foreach (var fragment in orderedFragments)
                {
                    labeldBuilder
                        .Append(label)
                        .Append(";Fragment: ")
                        .Append(fragment.CutPosition)
                        .Append("-")
                        .Append(fragment.CutPosition + fragment.Fragment.Length)
                        .Append(";Enzyme: ")
                        .Append(fragment.Enzyme)
                        .Append(";Length: ")
                        .Append(fragment.Fragment.Length)
                        .Append(";");
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