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
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using Microsoft.Win32;
using NuGet;
using Prism.Commands;
using Prism.Mvvm;

namespace BioinformaticsSuite.Module.ViewModels
{
    public abstract class SequenceViewModel : INotifyPropertyChanged
    {  
        // This class serves as the base class for all view models in the sequence view region,
        // to allow them to inherit commands, shared services and the INotifyPropertyChanged logic.

        private string textBoxInput;

        // Unity requires public class to resolve, do not make protected.
        public SequenceViewModel(ISequenceFactory sequenceFactory, ISequenceParser sequenceParser)
        {
            SequenceParser = sequenceParser;
            SequenceFactory = sequenceFactory;
            if(sequenceFactory == null) throw new ArgumentNullException(nameof(sequenceFactory));
            if(sequenceParser == null) throw new ArgumentNullException(nameof(sequenceParser)); 

            // ugly hacks to get the text box the right width, fix later
            ScreenHeight = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight - 100;
            ScreenWidth = System.Windows.SystemParameters.MaximizedPrimaryScreenWidth - 235;

            SubmitCommand = new DelegateCommand(OnSubmit);
            ClearCommand = new DelegateCommand(OnClear);
            UploadCommand = new DelegateCommand(OnUpload);
            SaveCommand = new DelegateCommand(OnSave);
        }

        public ISequenceFactory SequenceFactory { get; }
        public ISequenceParser SequenceParser { get; }

        public double ScreenHeight { get; }
        public double ScreenWidth { get; }

        public ICommand SubmitCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand UploadCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public string TextBoxInput
        {
            get { return textBoxInput; }
            set { SetProperty(ref textBoxInput, value); }
        }

        public virtual void OnSubmit() {}

        public void OnClear()
        {
            TextBoxInput = "";
        }

        public void OnUpload()
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
