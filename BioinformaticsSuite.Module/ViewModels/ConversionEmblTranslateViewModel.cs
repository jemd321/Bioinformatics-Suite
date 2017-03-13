﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Utility;
using Prism.Events;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class ConversionEmblTranslateViewModel : SequenceViewModel
    {
        private string _title = "EMBL to FASTA Protein converter";
        private readonly IEmblConverter _emblConverter;
        private readonly IEmblParser _emblParser;

        public ConversionEmblTranslateViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, IEventAggregator eventAggregator,
            IEmblConverter emblConverter, IEmblParser emblParser) : base(sequenceFactory, fastaParser, eventAggregator)
        {
            _emblConverter = emblConverter;
            _emblParser = emblParser;
            if (_emblConverter == null) throw new ArgumentNullException(nameof(emblConverter));
            if (_emblParser == null) throw new ArgumentNullException(nameof(emblParser));
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public override void OnRun()
        {
            const SequenceType sequenceType = SequenceType.Dna;
            if (_emblParser.TryParseEmblFile(InputBoxText))
            {
                List<string> emblRecords = _emblParser.EmblRecords;
                Dictionary<string, string> labelledFastas = _emblConverter.ConvertEmblFastaProtein(emblRecords);
                List<LabelledSequence> labelledSequences = SequenceFactory.CreateLabelledSequences(labelledFastas, SequenceType.Protein);
                ResultBoxText = BuildDisplayString(labelledSequences);
            }
            else
            {
                RaiseInvalidInputNotification(_emblParser.ErrorMessage);
                _emblParser.ResetSequences();
                return;
            }
            SelectedTab = SelectedTab.Result;
            _emblParser.ResetSequences();
        }
    }
}
