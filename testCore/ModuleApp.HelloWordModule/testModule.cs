using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using testModuleAppPrism.Models;
using testModuleAppPrism.Views;

namespace testModuleAppPrism
{
    public class testModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            this.Container.RegisterType<testMessageProvider>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<object, HelloWorldView>(nameof(HelloWorldView));

            this.RegionManager.RequestNavigate("MainRegion", nameof(HelloWorldView));
        }
    }
}
