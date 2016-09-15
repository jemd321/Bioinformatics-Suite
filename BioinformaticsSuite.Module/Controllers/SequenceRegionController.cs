using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace BioinformaticsSuite.Module.Controllers
{
    class SequenceRegionController
    {
        private IUnityContainer container;
        private IRegionManager regionManager;
        private IEventAggregator eventAggregator;

        public SequenceRegionController(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            this.container = container;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
        }

        private void MethodSelected(string methodName)
        {
            
        }


    }
}
