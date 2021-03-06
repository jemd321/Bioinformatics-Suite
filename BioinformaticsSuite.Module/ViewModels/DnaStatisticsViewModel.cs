﻿using System;
using System.Collections.Generic;
using System.Text;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Utility;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaStatisticsViewModel : SequenceViewModel
    {
        private readonly StringBuilder _displayStringBuilder = new StringBuilder();
        private string _title = "DNA Statistics";

        public DnaStatisticsViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator)
            : base(sequenceFactory, fastaParser, sequenceValidator)
        {
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public override void OnRun()
        {
            try
            {
                const SequenceType sequenceType = SequenceType.Dna;
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
                        sequenceType);
                    foreach (var labelledSequence in labelledSequences)
                    {
                        decimal sequenceLength = labelledSequence.Sequence.Length;
                        int[] baseCount = labelledSequence.Sequence.CountDnaBases();
                        decimal[] basePercent = CalculateBasePercentage(baseCount, sequenceLength);

                        BuildDisplayString(labelledSequence, sequenceLength, baseCount, basePercent);
                    }
                    ResultBoxText = _displayStringBuilder.ToString();
                    _displayStringBuilder.Clear();
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

        private static decimal[] CalculateBasePercentage(int[] baseCount, decimal sequenceLength)
        {
            decimal aPercent = baseCount[0]/sequenceLength*100;
            decimal cPercent = baseCount[1]/sequenceLength*100;
            decimal gPercent = baseCount[2]/sequenceLength*100;
            decimal tPercent = baseCount[3]/sequenceLength*100;
            return new[] {aPercent, cPercent, gPercent, tPercent};
        }

        // Concatenates labels and sequences for display in the sequence text box.
        private void BuildDisplayString(LabelledSequence labelledSequence, decimal sequenceLength, int[] baseCount,
            decimal[] basePercent)
        {
            int aCount = baseCount[0];
            int cCount = baseCount[1];
            int gCount = baseCount[2];
            int tCount = baseCount[3];
            decimal aPercent = basePercent[0];
            decimal cPercent = basePercent[1];
            decimal gPercent = basePercent[2];
            decimal tPercent = basePercent[3];


            _displayStringBuilder.AppendLine(labelledSequence.Label);
            _displayStringBuilder.Append("SequenceLength: ")
                .Append(sequenceLength)
                .Append("    Base Count:  A:   ")
                .Append(aCount)
                .Append("   C:   ")
                .Append(cCount)
                .Append("   G:   ")
                .Append(gCount)
                .Append("   T:   ")
                .Append(tCount)
                .Append("    Base Percent:   A:   ")
                .Append(Math.Round(aPercent, 2) + "%")
                .Append("   C:   ")
                .Append(Math.Round(cPercent, 2) + "%")
                .Append("   G:   ")
                .Append(Math.Round(gPercent, 2) + "%")
                .Append("   T:   ")
                .Append(Math.Round(tPercent, 2) + "%")
                .AppendLine();
        }
    }
}