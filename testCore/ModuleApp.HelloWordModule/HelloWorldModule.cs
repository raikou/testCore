using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using testModuleAppPrism.Models.Models;
using testModuleAppPrism.Models.Views;

namespace testModuleAppPrism.Models
{
    public class HelloWorldModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            this.Container.RegisterType<MessageProvider>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<object, HelloWorldView>(nameof(HelloWorldView));

            this.RegionManager.RequestNavigate("MainRegion", nameof(HelloWorldView));
        }
    }
}
