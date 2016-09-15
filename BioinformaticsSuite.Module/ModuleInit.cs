using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Controllers;
using BioinformaticsSuite.Module.Views;
using BioinformaticsSuite.Shell;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace BioinformaticsSuite.Module
{
    // All application logic, models, views etc goes here. 

    public class ModuleInit : IModule
    {        
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private SequenceRegionController sequenceRegionController;


        public ModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            this.regionManager.RegisterViewWithRegion(RegionNames.MethodSelctionRegion,
                () => this.container.Resolve<MethodSelectionView>());

            this.sequenceRegionController = this.container.Resolve<SequenceRegionController>();

            container.RegisterType<object, MethodSelectionView>("MethodSelectionView");
            container.RegisterType<object, DnaReadingFrameView>("DnaReadingFrameView");
        }

    }
}
