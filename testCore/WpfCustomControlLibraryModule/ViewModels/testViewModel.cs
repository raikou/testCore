using Microsoft.Practices.Unity;
using Prism.Interactivity.InteractionRequest;
using WpfCustomControlLibraryModule.Models;

namespace WpfCustomControlLibraryModule.ViewModels
{
	public class testViewModel
	{
		[Dependency]
		public LiblaryMessageProvider LiblaryMessageProvider { get; set; }

		public InteractionRequest<Notification> NotificationRequest { get; } = new InteractionRequest<Notification>();
	}
}