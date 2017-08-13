using Microsoft.Practices.Unity;
using testPrismModule.ViewModels;
using testPrismModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace testPrismModule
{
	public class testPrismModuleModule : IModule
	{
		[Dependency]
		public IUnityContainer Container { get; set; }

		[Dependency]
		public IRegionManager RegionManager { get; set; }

		public void Initialize()
		{
			this.Container.RegisterType<testModuleViewModel>(new ContainerControlledLifetimeManager());
			this.Container.RegisterType<object, testUserControl>(nameof(testUserControl));

			this.RegionManager.RequestNavigate("MainRegion", nameof(testUserControl));
		}
	}
}