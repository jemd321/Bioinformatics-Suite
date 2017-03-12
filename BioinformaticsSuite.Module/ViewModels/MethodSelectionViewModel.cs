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
        private readonly IRegionManager _regionManager;

        private bool _isChecked = false;

        public MethodSelectionViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this._regionManager = regionManager;
            SelectMethodCommand = new DelegateCommand<string>(OnMethodSelection);
        }

        public ICommand SelectMethodCommand { get; private set; }

        public bool IsChecked
        {
            get { return _isChecked;}
            set { SetProperty(ref _isChecked, value); }
        }

        private void OnMethodSelection(string methodName)
        {
            switch (methodName)
            {
                case MethodNames.DnaFindMotif:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaFindMotifView, UriKind.Relative));
                    break;
                case MethodNames.DnaMolecularWeight:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaMolecularWeightView, UriKind.Relative));
                    break;
                case MethodNames.DnaReadingFrame:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaReadingFrameView, UriKind.Relative));
                    break;
                case MethodNames.DnaRestrictionDigest:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaRestrictionDigestView, UriKind.Relative));
                    break;
                case MethodNames.DnaStatistics:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaStatisticsView, UriKind.Relative));
                    break;
                case MethodNames.DnaTranscribe:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaTranscribeView, UriKind.Relative));
                    break;
                case MethodNames.DnaTranslate:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.DnaTranslateView, UriKind.Relative));
                    break;

                case MethodNames.ProteinOpenReadingFrame:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.ProteinOpenReadingFrameView, UriKind.Relative));
                    break;
                case MethodNames.ProteinStatistics:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.ProteinStatisticsView, UriKind.Relative));
                    break;

                case MethodNames.ConversionFastaCombine:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.ConversionFastaCombineView, UriKind.Relative));
                    break;
                case MethodNames.ConversionFastaSplit:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.ConversionFastaSplitView, UriKind.Relative));
                    break;
                case MethodNames.ConversionEmblFasta:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.ConversionEmblFastaView, UriKind.Relative));
                    break;
                case MethodNames.ConversionEmblTranslate:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.ConversionEmblTranslateView, UriKind.Relative));
                    break;
                case MethodNames.ConversionGenbankFasta:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.ConversionGenbankFastaView, UriKind.Relative));
                    break;
                case MethodNames.ConversionGenbankTranslate:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion, new Uri(ViewNames.ConversionGenbankTranslateView, UriKind.Relative));
                    break;
            }
        }
    }

}
