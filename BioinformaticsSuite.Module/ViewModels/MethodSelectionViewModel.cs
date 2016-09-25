using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class MethodSelectionViewModel
    {
        private readonly IRegionManager regionManager;

        public MethodSelectionViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.SelectMethodCommand = new DelegateCommand<string>(OnMethodSelection);
        }

        public ICommand SelectMethodCommand { get; private set; }

        private void OnMethodSelection(string methodName)
        {
            switch (methodName)
            {
                case MethodNames.ReadingFrame:
                    regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaReadingFrameView, UriKind.Relative));
                    break;
                case MethodNames.Other:
                    break;
            }
        }
    }

}
