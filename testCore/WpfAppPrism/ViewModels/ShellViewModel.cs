using Microsoft.Practices.Unity;
using WpfAppPrism.Models;

namespace WpfAppPrism.ViewModels
{
	public class ShellViewModel
	{
		[Dependency]
		public MessageProvider MessageProvider { get; set; }
	}
}