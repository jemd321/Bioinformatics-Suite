﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaMolecularWeightViewModel : SequenceViewModel
    {
        private readonly IMolecularWeightCalculator molecularWeightCalculator;
        private string title = "DNA Molecular Weight";

        public DnaMolecularWeightViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator,
            IMolecularWeightCalculator molecularWeightCalculator) : base(sequenceFactory, sequenceParser, eventAggregator)
        {
            this.molecularWeightCalculator = molecularWeightCalculator;
            if (molecularWeightCalculator == null) throw new ArgumentNullException(nameof(molecularWeightCalculator));
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            bool isParsedSuccessfully = SequenceParser.TryParseInput(InputBoxText, sequenceType);
            if (isParsedSuccessfully)
            {
                var parsedSequences = SequenceParser.ParsedSequences;
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(parsedSequences, sequenceType);
                foreach (var labelledSequence in labelledSequences)
                {
                    molecularWeightCalculator.CalculateMolecularWeight(labelledSequence);
                }   
                ResultBoxText = BuildDisplayString(labelledSequences);
                SelectedTab = SelectedTab.Result;
            }
            else
            {
                MessageBoxResult errorMessageBox = MessageBox.Show(SequenceParser.ErrorMessage);
            }
            SequenceParser.ResetSequences();
        }

        // Concatenates labels and sequences for display in the sequence text box.
        public override string BuildDisplayString(List<LabelledSequence> labelledSequences)
        {
            StringBuilder displayStringBuilder = new StringBuilder();
            foreach (var labelledSequence in labelledSequences)
            {
                displayStringBuilder.AppendLine(labelledSequence.Label);
                displayStringBuilder.Append(DisplayStringSplitter(labelledSequence.MolecularWeight.ToString(CultureInfo.InvariantCulture)));
                displayStringBuilder.AppendLine(" Da");
            }
            var displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }
    }
}