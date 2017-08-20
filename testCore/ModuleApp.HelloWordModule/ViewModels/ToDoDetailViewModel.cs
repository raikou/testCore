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
using testCoreClassLibraryStandard;
using testModuleAppPrism.Models;
using testModuleAppPrism.Views;

namespace testModuleAppPrism.ViewModels
{
	public class ToDoDetailViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
	{
		#region Model情報
		ToDoList toDoList = new ToDoList();
		#endregion

		#region コマンド

		public DelegateCommand BackCommand { get; }

		#endregion

		#region パラメータ
		[Dependency]
		public IRegionManager RegionManager { get; set; }

		public bool KeepAlive { get; set; } = true;

		private PersonView selectItem = new PersonView();
		public PersonView SelectItem
		{
			get { return this.selectItem; }
			set { this.SetProperty(ref this.selectItem, value); }
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
			PersonView personView = navigationContext.Parameters["SelectItem"] as PersonView;

			//画面遷移時のデータ取得（ここで良いのかな？）
			this.SelectItem = toDoList.GetById(personView.id).Result;

			this.RegionManager = navigationContext.NavigationService.Region.RegionManager;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			Debug.WriteLine("OnNavigatedFrom");
		}

		#endregion
	}
}