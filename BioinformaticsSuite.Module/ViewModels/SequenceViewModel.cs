using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Popups;
using BioinformaticsSuite.Module.Services;
using Microsoft.Win32;
using NuGet;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace BioinformaticsSuite.Module.ViewModels
{
    public abstract class SequenceViewModel : INotifyPropertyChanged
    {
        private string _inputBoxText;
        private string _resultBoxText;
        private SelectedTab _selectedTab;
        private bool _validateSequences = true;
        // This class serves as the base class for all view models in the sequence view region,
        // to allow them to inherit commands, shared services, events and the INotifyPropertyChanged logic.

        private int _selectedTextBoxIndex;

        // Unity requires public constructors to resolve, do not make protected.
        public SequenceViewModel()
        {
        }

        public SequenceViewModel(ISequenceFactory sequenceFactory, IFastaParser fastaParser, ISequenceValidator sequenceValidator)
        {
            FastaParser = fastaParser;
            SequenceFactory = sequenceFactory;
            SequenceValidator = sequenceValidator;
            if (SequenceFactory == null) throw new ArgumentNullException(nameof(sequenceFactory));
            if (FastaParser == null) throw new ArgumentNullException(nameof(fastaParser));
            if (SequenceValidator == null) throw new ArgumentNullException(nameof(sequenceValidator));

            SequenceValidationRequest = new InteractionRequest<SequenceValidationNotification>();

            RunCommand = new DelegateCommand(OnRun);
            ClearCommand = new DelegateCommand(OnClear);
            OpenCommand = new DelegateCommand(OnOpen);
            SaveCommand = new DelegateCommand(OnSave);
        }

        public ISequenceFactory SequenceFactory { get; }
        public IFastaParser FastaParser { get; }
        public ISequenceValidator SequenceValidator { get; }

        public InteractionRequest<SequenceValidationNotification> SequenceValidationRequest { get; }

        public ICommand RunCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        // Dependency Property index for the selected tab, use the property 'Selected tab' to change this.
        public int SelectedTextBoxIndex
        {
            // index 0 = input tab, index 1 = result tab.
            get { return _selectedTextBoxIndex; }
            set
            {
                SetProperty(ref _selectedTextBoxIndex, value);
                switch (_selectedTextBoxIndex)
                {
                    case 0:
                        _selectedTab = SelectedTab.Input;
                        break;
                    case 1:
                        _selectedTab = SelectedTab.Result;
                        break;
                    default:
                        throw new Exception("Invalid sequence box tab index supplied");
                }
            }
        }

        // Set this property when you want to change the selected tab. This avoids having to use a 'magic number' tab index.
        public SelectedTab SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                switch (_selectedTab)
                {
                    case SelectedTab.Input:
                        SelectedTextBoxIndex = 0;
                        break;
                    case SelectedTab.Result:
                        SelectedTextBoxIndex = 1;
                        break;
                    default:
                        throw new Exception("Invalid sequence box tab enum selected");
                }
            }
        }

        public string InputBoxText
        {
            get { return _inputBoxText; }
            set { SetProperty(ref _inputBoxText, value); }
        }

        public string ResultBoxText
        {
            get { return _resultBoxText; }
            set { SetProperty(ref _resultBoxText, value); }
        }

        public bool ValidateSequences
        {
            get { return _validateSequences; }
            set { SetProperty(ref _validateSequences, value); }
        }

        // The following events and methods are a copy of the bindable base PRISM class, which allows viewmodels to inherit the dependency
        // property logic for INotifyPropertyChanged. This has been copied here since you can't inherit from multiple classes, do not edit!

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnRun()
        {
        }

        public void OnClear()
        {
            switch (_selectedTab)
            {
                case SelectedTab.Input:
                    InputBoxText = "";
                    break;
                case SelectedTab.Result:
                    ResultBoxText = "";
                    break;
                default:
                    throw new Exception("Invalid sequence box tab selected for clear dialog");
            }
        }

        public void OnOpen()
        {
            switch (_selectedTab)
            {
                case SelectedTab.Input:
                    OpenFile();
                    break;
                case SelectedTab.Result:
                    SelectedTab = SelectedTab.Input;
                    OpenFile();
                    break;
                default:
                    throw new Exception("Invalid sequence box tab selected for open dialog");
            }
        }

        public void OnSave()
        {
            var dialog = new SaveFileDialog {OverwritePrompt = true, Filter = "FASTA File (*.txt)|*.txt"};
            var result = dialog.ShowDialog();
            if (string.IsNullOrWhiteSpace(dialog.FileName)) return;
            using (var writer = new StreamWriter(dialog.FileName))
            {
                switch (_selectedTab)
                {
                    case SelectedTab.Input:
                        writer.WriteLine(InputBoxText);
                        break;
                    case SelectedTab.Result:
                        writer.WriteLine(ResultBoxText);
                        break;
                    default:
                        throw new Exception("Invalid sequence box tab selected for save dialog");
                }
            }
        }

        private void OpenFile()
        {
            var dialog = new OpenFileDialog {Filter = "FASTA File (*.txt)|*.txt"};
            var result = dialog.ShowDialog();
            if (result == false) return;

            using (var reader = dialog.OpenFile())
            {
                InputBoxText = reader.ReadToEnd();
            }
        }

        protected void RaiseInvalidInputNotification(ValidationErrorMessage errorMessage)
        {
            SequenceValidationRequest.Raise(new SequenceValidationNotification(errorMessage));
        }

        public virtual string BuildDisplayString(List<LabelledSequence> labelledSequences)
        {
            var displayStringBuilder = new StringBuilder();
            foreach (var labelledSequence in labelledSequences)
            {
                displayStringBuilder.AppendLine(labelledSequence.Label);
                displayStringBuilder.AppendLine(labelledSequence.Sequence);
            }
            var displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }
    }
}