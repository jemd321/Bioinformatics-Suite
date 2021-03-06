﻿using System;
using System.Windows;
using System.Windows.Input;
using BioinformaticsSuite.Module.Enums;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BioinformaticsSuite.Module.ViewModels
{
    public class MethodSelectionViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private double _methodSelectionViewWidth = 220;
        private Visibility _methodsVisibility = Visibility.Visible; 
        private Visibility _expandButtonVisibility = Visibility.Collapsed;

        public MethodSelectionViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ResizeMethodSelectionViewCommand = new DelegateCommand(OnResizeMethodSelectionView);
            SelectMethodCommand = new DelegateCommand<string>(OnMethodSelection);
        }

        public ICommand SelectMethodCommand { get; private set; }
        public ICommand ResizeMethodSelectionViewCommand { get; private set; }

        public double MethodSelectionViewWidth
        {
            get { return _methodSelectionViewWidth; }
            set { SetProperty(ref _methodSelectionViewWidth, value); }
        }

        public Visibility MethodsVisibility
        {
            get { return _methodsVisibility; }
            set { SetProperty(ref _methodsVisibility, value); }
        }

        public Visibility ExpandButtonVisibility
        {
            get { return _expandButtonVisibility; }
            set { SetProperty(ref _expandButtonVisibility, value); }
        }



        private void OnResizeMethodSelectionView()
        {
            if (MethodsVisibility == Visibility.Visible)
            {
                // Hide methods and make expand button visible
                MethodSelectionViewWidth = 30;
                MethodsVisibility = Visibility.Collapsed;
                ExpandButtonVisibility = Visibility.Visible;
            }
            else
            {
                // Expand Methods and hide expand button
                MethodSelectionViewWidth = 220;
                MethodsVisibility = Visibility.Visible;
                ExpandButtonVisibility = Visibility.Collapsed;
            }
        }

        private void OnMethodSelection(string methodName)
        {
            switch (methodName)
            {
                // Dna Methods
                case MethodNames.DnaFindMotif:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.DnaFindMotifView, UriKind.Relative));
                    break;
                case MethodNames.DnaMolecularWeight:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.DnaMolecularWeightView, UriKind.Relative));
                    break;
                case MethodNames.DnaReadingFrame:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.DnaReadingFrameView, UriKind.Relative));
                    break;
                case MethodNames.DnaRestrictionDigest:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.DnaRestrictionDigestView, UriKind.Relative));
                    break;
                case MethodNames.DnaStatistics:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.DnaStatisticsView, UriKind.Relative));
                    break;
                case MethodNames.DnaTranscribe:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.DnaTranscribeView, UriKind.Relative));
                    break;
                case MethodNames.DnaTranslate:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.DnaTranslateView, UriKind.Relative));
                    break;

                // Rna Methods
                case MethodNames.RnaTranslate:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.RnaTranslateView, UriKind.Relative));
                    break;
                case MethodNames.RnaMolecularWeight:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.RnaMolecularWeightView, UriKind.Relative));
                    break;

                // Protein Methods
                case MethodNames.ProteinOpenReadingFrame:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.ProteinOpenReadingFrameView, UriKind.Relative));
                    break;
                case MethodNames.ProteinStatistics:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.ProteinStatisticsView, UriKind.Relative));
                    break;
                case MethodNames.ProteinMolecularWeight:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.ProteinMolecularWeightView, UriKind.Relative));
                    break;

                // Conversion Methods
                case MethodNames.ConversionFastaCombine:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.ConversionFastaCombineView, UriKind.Relative));
                    break;
                case MethodNames.ConversionFastaSplit:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.ConversionFastaSplitView, UriKind.Relative));
                    break;
                case MethodNames.ConversionEmblFasta:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.ConversionEmblFastaView, UriKind.Relative));
                    break;
                case MethodNames.ConversionEmblTranslate:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.ConversionEmblTranslateView, UriKind.Relative));
                    break;
                case MethodNames.ConversionGenbankFasta:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.ConversionGenbankFastaView, UriKind.Relative));
                    break;
                case MethodNames.ConversionGenbankTranslate:
                    _regionManager.RequestNavigate(RegionNames.SequenceRegion,
                        new Uri(ViewNames.ConversionGenbankTranslateView, UriKind.Relative));
                    break;
            }
        }
    }
}