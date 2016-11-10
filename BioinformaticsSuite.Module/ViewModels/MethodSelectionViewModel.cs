using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BioinformaticsSuite.Module.Enums;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class MethodSelectionViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        private bool isChecked = false;

        public MethodSelectionViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            SelectMethodCommand = new DelegateCommand<string>(OnMethodSelection);
        }

        public ICommand SelectMethodCommand { get; private set; }

        public bool IsChecked
        {
            get { return isChecked;}
            set { SetProperty(ref isChecked, value); }
        }

        private void OnMethodSelection(string methodName)
        {
            switch (methodName)
            {
                case MethodNames.DnaFindMotif:
                    regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaFindMotifView, UriKind.Relative));
                    break;
                case MethodNames.DnaMolecularWeight:
                    regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaMolecularWeightView, UriKind.Relative));
                    break;
                case MethodNames.DnaReadingFrame:
                    regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaReadingFrameView, UriKind.Relative));
                    break;
                case MethodNames.DnaRestrictionDigest:
                    regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaRestrictionDigestView, UriKind.Relative));
                    break;
                case MethodNames.DnaStatistics:
                    regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaStatisticsView, UriKind.Relative));
                    break;
                case MethodNames.DnaTranscribe:
                    regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaTranscribeView, UriKind.Relative));
                    break;
                case MethodNames.DnaTranslate:
                    regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaTranslateView, UriKind.Relative));
                    break;
                case MethodNames.ProteinOpenReadingFrame:
                    regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.ProteinOpenReadingFrameView, UriKind.Relative));
                    break;
            }
        }
    }

}
