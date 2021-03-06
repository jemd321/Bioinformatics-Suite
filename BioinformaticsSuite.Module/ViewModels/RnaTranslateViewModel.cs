﻿using System.Collections.Generic;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Utility;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class RnaTranslateViewModel : SequenceViewModel
    {
        private string _title = "Translate RNA";

        public RnaTranslateViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator)
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
                const SequenceType sequenceType = SequenceType.Rna;
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
                    var translatedSequences = new Dictionary<string, string>();
                    foreach (var labelledSequence in parsedSequences)
                    {
                        string rnaSequence = labelledSequence.Value;
                        string proteinSequence = Translation.TranslateRnaToProtein(rnaSequence);
                        translatedSequences.Add(labelledSequence.Key, proteinSequence);
                    }
                    List<LabelledSequence> labelledProteins = SequenceFactory.CreateLabelledSequences(translatedSequences,
                        SequenceType.Protein);
                    ResultBoxText = BuildDisplayString(labelledProteins);
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
    }
}