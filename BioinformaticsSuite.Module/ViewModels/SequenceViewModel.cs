using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BioinformaticsSuite.Module.Events;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Microsoft.Win32;
using NuGet;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace BioinformaticsSuite.Module.ViewModels
{
    public abstract class SequenceViewModel : INotifyPropertyChanged
    {  
        // This class serves as the base class for all view models in the sequence view region,
        // to allow them to inherit commands, shared services, events and the INotifyPropertyChanged logic.
        private double textBoxHeight;
        private double textBoxWidth;
        private string textBoxInput;

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
            TextBoxHeight = Application.Current.MainWindow.ActualHeight - infoAndTitleBarHeight;
            TextBoxWidth = Application.Current.MainWindow.ActualWidth - methodSelectionBarWidth;

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

        public string TextBoxInput
        {
            get { return textBoxInput; }
            set { SetProperty(ref textBoxInput, value); }
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

        public void AdjustTextBoxHeight(WindowSize currentWindowSize)
        {
            TextBoxHeight = currentWindowSize.Height;
            TextBoxWidth = currentWindowSize.Width;
        }

        public virtual void SubscribeToEvents()
        {
            EventAggregator.GetEvent<WindowSizeChanged>().Subscribe(AdjustTextBoxHeight, ThreadOption.UIThread);
        }

        public virtual void OnRun() {}

        public void OnClear()
        {
            TextBoxInput = "";
        }


        // change upload/save to WPF dialog rather than Win32 dialog, since the latter is very difficult to test properly.
        public void OnOpen()
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "FASTA File (*.txt)|*.txt" };
            var result = dialog.ShowDialog();
            if (result == false) return;

            using (Stream reader = dialog.OpenFile())
            {
                TextBoxInput = reader.ReadToEnd();
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
                    writer.WriteLine(TextBoxInput);
                }
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
