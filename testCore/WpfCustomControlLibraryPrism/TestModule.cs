using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using WpfCustomControlLibraryPrism.Models;
using WpfCustomControlLibraryPrism.Views;

namespace WpfCustomControlLibraryPrism
{
	public class TestModule :IModule
	{
		[Dependency]
		public IUnityContainer Container { get; set; }

		[Dependency]
		public IRegionManager RegionManager { get; set; }

		public void Initialize()
		{
			this.Container.RegisterType<MessageProvider>(new ContainerControlledLifetimeManager());
			this.Container.RegisterType<object, UserControlView>(nameof(UserControlView));

			this.RegionManager.RequestNavigate("MainRegion", nameof(UserControlView));
		}
	}
}