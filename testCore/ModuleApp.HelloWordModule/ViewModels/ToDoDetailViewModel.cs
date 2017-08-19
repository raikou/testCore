using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Regions;
using testModuleAppPrism.Views;

namespace testModuleAppPrism.ViewModels
{
	public class ToDoDetailViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
	{
		#region コマンド

		public DelegateCommand BackCommand { get; }

		#endregion

		#region パラメータ
		[Dependency]
		public IRegionManager RegionManager { get; set; }

		public bool KeepAlive { get; set; } = true;

		private string id;

		public string Id
		{
			get { return this.id; }
			set { this.SetProperty(ref this.id, value); }
		}
		#endregion

		#region コンストラクタ

		public ToDoDetailViewModel()
		{
			BackCommand = new DelegateCommand(() =>
			{
				this.KeepAlive = false;
				// find view by region
				var view = this.RegionManager.Regions["MainRegion"]
					.ActiveViews
					.First(x => MvvmHelpers.GetImplementerFromViewOrViewModel<ToDoDetailViewModel>(x) == this);
				// deactive view
				this.RegionManager.Regions["MainRegion"].Deactivate(view);

				this.RegionManager.RequestNavigate("MainRegion", nameof(ToDoListCommandView));
			});

		}

		#endregion


		#region ナビゲーション
		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			Debug.WriteLine("IsNavigationTarget");
		return true;
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			Debug.WriteLine("OnNavigatedTo");
			this.Id = navigationContext.Parameters["id"] as string;
			this.RegionManager = navigationContext.NavigationService.Region.RegionManager;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			Debug.WriteLine("OnNavigatedFrom");
		}

		#endregion
	}
}