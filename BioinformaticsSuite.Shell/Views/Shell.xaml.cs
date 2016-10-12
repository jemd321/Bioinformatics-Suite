using System;
using System.Windows;
using BioinformaticsSuite.Module;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Events;
using Prism.Events;

namespace BioinformaticsSuite.Shell.Views
{
    public partial class Shell : Window
    {
        private IEventAggregator eventAggregator;
        private WindowSize currentWindowSize;

        // These events publish the new dimensions of the main window whenever it is resized.
        // this is broadcast on the UI thread to UI elements that need to resize dynamically.
        public Shell(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            currentWindowSize = new WindowSize();
            this.eventAggregator = eventAggregator;
            SizeChanged += OnWindowSizeChanged;
        }

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            currentWindowSize.Height = e.NewSize.Height;
            currentWindowSize.Width = e.NewSize.Width;

            eventAggregator.GetEvent<WindowSizeChanged>().Publish(currentWindowSize);
        }
    }
}
