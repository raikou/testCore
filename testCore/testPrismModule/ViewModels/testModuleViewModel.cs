
using Microsoft.Practices.Unity;
using testPrismModule.Models;

namespace testPrismModule.ViewModels
{
	class testModuleViewModel
	{
		[Dependency]
		public testModuleModel MessageProvider { get; set; }
	}
}