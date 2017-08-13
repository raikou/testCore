using Microsoft.Practices.Unity;
using WpfCustomControlLibraryModule.Views;
using WpfCustomControlLibraryModule.Properties;
using Prism.Modularity;
using Prism.Regions;
using WpfCustomControlLibraryModule.Models;

namespace WpfCustomControlLibraryModule
{
	public class testModule : IModule
	{
		[Dependency]
		public IUnityContainer Container { get; set; }

		[Dependency]
		public IRegionManager RegionManager { get; set; }

		public void Initialize()
		{
			this.Container.RegisterType<LiblaryMessageProvider>(new ContainerControlledLifetimeManager());
			this.Container.RegisterType<object, testView>(nameof(testView));

			this.RegionManager.RequestNavigate("MainRegion", nameof(testView));
		}
	}
}