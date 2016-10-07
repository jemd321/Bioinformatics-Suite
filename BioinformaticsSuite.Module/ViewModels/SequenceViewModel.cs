﻿using System;
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
using BioinformaticsSuite.Module.Events;
using BioinformaticsSuite.Module.Services;
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
        private double textBoxHeight;
        private double textBoxWidth;
        private double maxInfoBarWidth;

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
          
            // Event Subscription is done via virtual method rather than here in the constructor for testing purposes,
            // since mock view models cannot be instantiated when the UIThread has not been constructed. Only testing mocks override this method.
            SubscribeToEvents();

            // Initial Max Dimensions Assignment
            const int infoAndTitleBarHeight = 80;
            const int methodSelectionBarWidth = 235;
            const int infoBarButtonsWidth = 200;
            TextBoxHeight = Application.Current.MainWindow.ActualHeight - infoAndTitleBarHeight;
            TextBoxWidth = Application.Current.MainWindow.ActualWidth - methodSelectionBarWidth;
            MaxInfoBarWidth = Application.Current.MainWindow.ActualWidth - infoBarButtonsWidth - methodSelectionBarWidth;
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

        public double TextBoxHeight
        {
            get { return textBoxHeight; }
            set { SetProperty(ref textBoxHeight, value - 50); }
        }
        public double TextBoxWidth
        {
            get { return textBoxWidth; }
            set { SetProperty(ref textBoxWidth, value); }
        }

        public double MaxInfoBarWidth
        {
            get { return maxInfoBarWidth; }
            set { SetProperty(ref maxInfoBarWidth, value); }
        }

        public void AdjustTextBoxHeight(WindowSize currentWindowSize)
        {
            TextBoxHeight = currentWindowSize.Height;
            TextBoxWidth = currentWindowSize.Width;
            MaxInfoBarWidth = currentWindowSize.Width - 200;
        }

        public virtual void SubscribeToEvents()
        {
            EventAggregator.GetEvent<WindowSizeChanged>().Subscribe(AdjustTextBoxHeight, ThreadOption.UIThread);
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
