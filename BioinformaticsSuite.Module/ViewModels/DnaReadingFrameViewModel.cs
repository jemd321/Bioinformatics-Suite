using System.Collections.Generic;
using System.Text;
using BioinformaticsSuite.Module.Models;
using Prism.Mvvm;
using Prism.Regions;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class DnaReadingFrameViewModel
    {
        public DnaReadingFrameViewModel()
        {
            
        }









        /*
        private string _textBoxInput;
        private IReadingFrame IreadingFrame;

        public string Sequence
        {
            get { return _textBoxInput; }
            set
            {
                SetProperty(ref _textBoxInput, value);
            }
        }

        public SequenceViewModel(IReadingFrame IreadingFrame)
        {
            this.IreadingFrame = IreadingFrame;
        }

        public void DoStuff()
        {
            var parser = new InputParser(_textBoxInput);
            var parsedInput = parser.ParsedSequences;
            List<Dna> dnaList = new List<Dna>();
            List<ReadingFrame> frameList = new List<ReadingFrame>();

            foreach (KeyValuePair<string, string> pair in parsedInput)
            {
                string label = pair.Key;
                string sequence = pair.Value;

                dnaList.Add(new Dna(label, sequence));
            }

            foreach (Dna dna in dnaList)
            {
                frameList.Add(new ReadingFrame(dna));
            }

            StringBuilder builder = new StringBuilder();
            foreach (ReadingFrame frames in frameList)
            {
                foreach (KeyValuePair<string, string> frame in frames.LabelledFrames)
                {
                    builder.Append(frame.Key);
                    builder.AppendLine();
                    builder.Append(frame.Value);
                    builder.AppendLine();
                }
            }

            _textBoxInput = builder.ToString();
            builder.Clear();





            //use interface methods to set the sequence field, which the specialised dependency property will then update to reflect the new value.
            
        }
        */
    }
}
