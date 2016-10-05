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

        // these are hacks so that the size of the elements surrounding the sequence text box are account for when resizing.
        private const double infoAndTitleBarHeight = 80;
        private const double methodSelectionBarWidth = 235;

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
            currentWindowSize.Height = e.NewSize.Height - infoAndTitleBarHeight;
            currentWindowSize.Width = e.NewSize.Width - methodSelectionBarWidth;

            eventAggregator.GetEvent<WindowSizeChanged>().Publish(currentWindowSize);
        }
    }
}
