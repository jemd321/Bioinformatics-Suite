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
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public ModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            // View Registration
            container.RegisterType<object, MethodSelectionView>("MethodSelectionView");
            container.RegisterType<object, DnaReadingFrameView>("DnaReadingFrameView");
            container.RegisterType<object, IntroView>("IntroView");

            // Service Registration
            container.RegisterType<IReadingFrameFactory, ReadingFrameFactory>();
            container.RegisterType<ISequenceFactory, SequenceFactory>();
            container.RegisterType<ISequenceParser, SequenceParser>();
            container.RegisterType<ISequenceValidator, SequenceValidator>();

            // StartUp Views Registration
            regionManager.RegisterViewWithRegion(RegionNames.MethodSelectionRegion,
                () => this.container.Resolve<MethodSelectionView>());
            regionManager.RegisterViewWithRegion(RegionNames.SequenceRegion,
                () => container.Resolve<IntroView>());
        }

    }
}
