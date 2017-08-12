using Microsoft.Practices.Unity;
using Prism.Interactivity.InteractionRequest;
using WpfAppPrism.Models;

namespace WpfAppPrism.ViewModels
{
	public class ShellViewModel
	{
		[Dependency]
		public MessageProvider MessageProvider { get; set; }

		public InteractionRequest<Notification> NotificationRequest { get; } = new InteractionRequest<Notification>();
	}
}