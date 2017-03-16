using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Models;
using BioinformaticsSuite.Module.Services;
using BioinformaticsSuite.Module.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace BioinformaticsSuite.Module
{
    public class ModuleInit : IModule
    {        
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public ModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            // View Registration
            _container.RegisterType<object, MethodSelectionView>("MethodSelectionView");
            _container.RegisterType<object, IntroView>("IntroView");

            _container.RegisterType<object, DnaFindMotifView>(ViewNames.DnaFindMotifView);
            _container.RegisterType<object, DnaMolecularWeightView>(ViewNames.DnaMolecularWeightView);
            _container.RegisterType<object, DnaReadingFrameView>(ViewNames.DnaReadingFrameView);
            _container.RegisterType<object, DnaRestrictionDigestView>(ViewNames.DnaRestrictionDigestView);
            _container.RegisterType<object, DnaStatisticsView>(ViewNames.DnaStatisticsView);
            _container.RegisterType<object, DnaTranscribeView>(ViewNames.DnaTranscribeView);
            _container.RegisterType<object, DnaTranslateView>(ViewNames.DnaTranslateView);

            _container.RegisterType<object, RnaTranslateView>(ViewNames.RnaTranslateView);
            _container.RegisterType<object, RnaMolecularWeightView>(ViewNames.RnaMolecularWeightView);

            _container.RegisterType<object, ProteinOpenReadingFrameView>(ViewNames.ProteinOpenReadingFrameView);
            _container.RegisterType<object, ProteinStatisticsView>(ViewNames.ProteinStatisticsView);
            _container.RegisterType<object, ProteinMolecularWeightView>(ViewNames.ProteinMolecularWeightView);

            _container.RegisterType<object, ConversionFastaCombineView>(ViewNames.ConversionFastaCombineView);
            _container.RegisterType<object, ConversionFastaSplitView>(ViewNames.ConversionFastaSplitView);
            _container.RegisterType<object, ConversionEmblFastaView>(ViewNames.ConversionEmblFastaView);
            _container.RegisterType<object, ConversionEmblTranslateView>(ViewNames.ConversionEmblTranslateView);
            _container.RegisterType<object, ConversionGenbankFastaView>(ViewNames.ConversionGenbankFastaView);
            _container.RegisterType<object, ConversionGenbankTranslateView>(ViewNames.ConversionGenbankTranslateView);

            // Service Registration
            _container.RegisterType<IReadingFrameFactory, ReadingFrameFactory>();
            _container.RegisterType<ISequenceFactory, SequenceFactory>();
            _container.RegisterType<IFastaParser, FastaParser>();
            _container.RegisterType<ISequenceValidator, SequenceValidator>();
            _container.RegisterType<IMolecularWeightCalculator, MolecularWeightCalculator>();
            _container.RegisterType<IMotifFinder, MotifFinder>();
            _container.RegisterType<IRestrictionDigest, RestrictionDigest>();
            _container.RegisterType<IOpenReadingFrameFinder, OpenReadingFrameFinder>();
            _container.RegisterType<IFastaManipulator, FastaManipulator>();
            _container.RegisterType<IGenbankConverter, GenbankConverter>();
            _container.RegisterType<IGenbankParser, GenbankParser>();
            _container.RegisterType<IEmblConverter, EmblConverter>();
            _container.RegisterType<IEmblParser, EmblParser>();

            // StartUp Views Registration
            _regionManager.RegisterViewWithRegion(RegionNames.MethodSelectionRegion,
                () => _container.Resolve<MethodSelectionView>());
            _regionManager.RegisterViewWithRegion(RegionNames.SequenceRegion,
                () => _container.Resolve<IntroView>());
        }

    }
}
