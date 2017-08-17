using Prism.Modularity;
using Prism.Regions;
using System;

namespace testPrismModuleWPF
{
	public class testPrismModuleWPFModule : IModule
	{
		IRegionManager _regionManager;

		public testPrismModuleWPFModule(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			throw new NotImplementedException();
		}
	}
}