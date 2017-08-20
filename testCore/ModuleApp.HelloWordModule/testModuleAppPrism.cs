using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using testModuleAppPrism.Models;
using testModuleAppPrism.ViewModels;
using testModuleAppPrism.Views;

namespace testModuleAppPrism
{
    public class testModuleAppPrism : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            this.Container.RegisterType<ToDoList>(new ContainerControlledLifetimeManager());
	        this.Container.RegisterType<object, ToDoListCommandView>(nameof(ToDoListCommandView));
	        this.Container.RegisterType<object, ToDoDetailView>(nameof(ToDoDetailView));

			this.RegionManager.RequestNavigate("MainRegion", nameof(ToDoListCommandView));
	        //this.RegionManager.RequestNavigate("MainRegion", nameof(ToDoDetailView));
		}
	}
}
