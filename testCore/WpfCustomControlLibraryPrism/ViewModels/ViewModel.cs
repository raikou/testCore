using Microsoft.Practices.Unity;
using WpfCustomControlLibraryPrism.Models;

namespace WpfCustomControlLibraryPrism.ViewModels
{
	public class ViewModel
	{
		[Dependency]
		public MessageProvider MessageProvider { get; set; }
	}
}