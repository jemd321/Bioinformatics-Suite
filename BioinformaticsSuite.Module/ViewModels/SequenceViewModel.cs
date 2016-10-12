using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Views;
using Microsoft.Win32;
using NuGet;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace BioinformaticsSuite.Module.ViewModels
{
    public abstract class SequenceViewModel : INotifyPropertyChanged
    {  
        // This class serves as the base class for all view models in the sequence view region,
        // to allow them to inherit commands, shared services, events and the INotifyPropertyChanged logic.

        private int selectedTextBoxIndex;
        private SelectedTab selectedTab;
        private string inputBoxText;
        private string resultBoxText;

        // Unity requires public constructors to resolve, do not make protected.
        public SequenceViewModel() { }

        public SequenceViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser, IEventAggregator eventAggregator)
        {
            SequenceParser = sequenceParser;
            SequenceFactory = sequenceFactory;
            EventAggregator = eventAggregator;
            if(SequenceFactory == null) throw new ArgumentNullException(nameof(sequenceFactory));
            if(SequenceParser == null) throw new ArgumentNullException(nameof(sequenceParser));
            if(EventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
          
            RunCommand = new DelegateCommand(OnRun);
            ClearCommand = new DelegateCommand(OnClear);
            OpenCommand = new DelegateCommand(OnOpen);
            SaveCommand = new DelegateCommand(OnSave);            
        }

        public ISequenceFactory SequenceFactory { get; }
        public ISequenceParser SequenceParser { get; }
        public IEventAggregator EventAggregator { get; }

        public ICommand RunCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        // Dependency Property index for the selected tab, use the enum 'Selected tab' to change this.
        public int SelectedTextBoxIndex
        {
            // index 0 = input tab, index 1 = result tab.
            get { return selectedTextBoxIndex; }
            set
            {
                SetProperty(ref selectedTextBoxIndex, value);
                switch (selectedTextBoxIndex)
                {
                    case 0:
                        selectedTab = SelectedTab.Input;
                        break;
                    case 1:
                        selectedTab = SelectedTab.Result;
                        break;
                }
            }
        }

        // Set this property when you want to change the selected tab. This avoids having to use the magic number tab index.
        public SelectedTab SelectedTab
        {
            get { return selectedTab; }
            set
            {
                selectedTab = value;
                switch (selectedTab)
                {
                    case SelectedTab.Input:
                        SelectedTextBoxIndex = 0;
                        break;
                    case SelectedTab.Result:
                        SelectedTextBoxIndex = 1;
                        break;
                }
            }
        }

        public string InputBoxText
        {
            get { return inputBoxText; }
            set { SetProperty(ref inputBoxText, value); }
        }

        public string ResultBoxText
        {
            get { return resultBoxText; }
            set { SetProperty(ref resultBoxText, value); }
        }

        public virtual void OnRun() {}

        public void OnClear()
        {
            switch (selectedTab)
            {
                case SelectedTab.Input:
                    InputBoxText = "";
                    break;
                case SelectedTab.Result:
                    ResultBoxText = "";
                    break;
            }
        }

        // change open/save to WPF dialog  at some point rather than Win32 dialog, since the latter is very difficult to test properly.
        public void OnOpen()
        {
            switch (selectedTab)
            {
                case SelectedTab.Input:
                    OpenFile();
                    break;
                case SelectedTab.Result:
                    SelectedTab = SelectedTab.Input;
                    OpenFile();
                    break;
            }
        }

        public void OnSave()
        {
            SaveFileDialog dialog = new SaveFileDialog { OverwritePrompt = true, Filter = "FASTA File (*.txt)|*.txt" };
            var result = dialog.ShowDialog();
            if (!string.IsNullOrWhiteSpace(dialog.FileName))
            {
                using (StreamWriter writer = new StreamWriter(dialog.FileName))
                {
                    switch (selectedTab)
                    {
                        case SelectedTab.Input:
                            writer.WriteLine(InputBoxText);
                            break;
                        case SelectedTab.Result:
                            writer.WriteLine(ResultBoxText);
                            break;
                    }
                }
            }
        }

        private void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "FASTA File (*.txt)|*.txt" };
            var result = dialog.ShowDialog();
            if (result == false) return;

            using (Stream reader = dialog.OpenFile())
            {
                InputBoxText = reader.ReadToEnd();
            }
        }

        // Currently unused -  can't decide on whether to have the sequences wrap inside the text box or to insert line breaks. -  wrapping 
        // messes with the line numbers though.
        public string DisplayStringSplitter(string line)
        {
            StringBuilder displayStringBuilder = new StringBuilder();
            int length = line.Length;

            // To be settable in options later
            const int maxLineLength = 70;

            int startIndex = 0;
            int endIndex = maxLineLength;
            while (length > endIndex)
            {
                displayStringBuilder.AppendLine(line.Substring(startIndex, maxLineLength));
                startIndex += maxLineLength;
                endIndex += maxLineLength;
            }
            if (length % 80 != 0)
            {
                int remainingLength = length - startIndex;
                displayStringBuilder.Append(line.Substring(startIndex, remainingLength));
            }
            line = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return line;
        }

        public virtual string BuildDisplayString(List<LabelledSequence> labelledSequences)
        {
            StringBuilder displayStringBuilder = new StringBuilder();
            foreach (var labelledSequence in labelledSequences)
            {
                displayStringBuilder.AppendLine(labelledSequence.Label);
                displayStringBuilder.AppendLine(DisplayStringSplitter(labelledSequence.Sequence));
            }
            string displayString = displayStringBuilder.ToString();
            displayStringBuilder.Clear();
            return displayString;
        }

        // The following events and methods are a copy of the bindable base PRISM class, which allows viewmodels to inherit the dependency
        // property logic for INotifyPropertyChanged. This has been copied here since you can't inherit from multiple classes, do not edit!

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            this.OnPropertyChanged(propertyName);
        }
    }  
}
