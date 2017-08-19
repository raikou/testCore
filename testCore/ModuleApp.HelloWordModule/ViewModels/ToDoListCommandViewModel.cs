﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Common;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using testCoreClassLibraryStandard;
using testModuleAppPrism.Models;
using testModuleAppPrism.Views;


namespace testModuleAppPrism.ViewModels
{
	public class ToDoListCommandViewModel :BindableBase, IRegionMemberLifetime, INavigationAware
	{
		public string HeaderText { get; } = "ToDoListCommand";



		#region Model情報
		ToDoList toDoList = new ToDoList();
		#endregion

		#region パラメータ
		[Dependency]
		public IRegionManager RegionManager { get; set; }
		public bool KeepAlive { get; set; } = true;
		#endregion

		#region 通知ダイアログ
		/// <summary>
		/// メッセージ用（OKのみ）
		/// </summary>
		public InteractionRequest<Notification> NotificationRequest { get; } = new InteractionRequest<Notification>();
		#endregion
		#region コマンドリスト
		public DelegateCommand GetMainListCommand { get; }
		public DelegateCommand AddCommand { get; }
		public DelegateCommand UpdCommand { get; }
		public DelegateCommand DelCommand { get; }
		public DelegateCommand DetailCommand { get; }
		#endregion

		#region コマンドで利用する画面情報

		private List<PersonView> gridItem = new List<PersonView>();
		public List<PersonView> GridItem
		{
			get { return this.gridItem; }
			set { this.SetProperty(ref this.gridItem, value); }
		}

		private PersonView selectItem = new PersonView();
		public PersonView SelectItem
		{
			get { return this.selectItem; }
			set { this.SetProperty(ref this.selectItem, value); }
		}


		#endregion
		public ToDoListCommandViewModel()
		{
			//初期データ取得
			GridItem = toDoList.GetUserList().Result;

			//コマンド生成
			this.GetMainListCommand = new DelegateCommand(() =>
			{
				GridItem = toDoList.GetUserList().Result;
			});
			this.AddCommand = new DelegateCommand(() =>
			{
				PersonView person = new PersonView();
				person.name = "新規データ";
				person.age = 0;
				toDoList.Post(person, GridItem );

				GridItem = toDoList.GetUserList().Result;
			});
			this.UpdCommand = new DelegateCommand(() =>
			{
				this.NotificationRequest.Raise(new Notification { Title = "Alert", Content = "未実装です" });
			});
			this.DelCommand = new DelegateCommand(() =>
			{
				this.NotificationRequest.Raise(new Notification { Title = "Alert", Content = "未実装です" });
			});
			this.DetailCommand = new DelegateCommand(() =>
			{
				this.KeepAlive = false;
				//TODO:情報残るようなら以下のコメントを消す
				//// find view by region
				//var view = RegionManager.Regions["MainRegion"]
				//	.ActiveViews
				//	.First(x => MvvmHelpers.GetImplementerFromViewOrViewModel<ToDoListCommandViewModel>(x) == this);
				//// deactive view
				//this.RegionManager.Regions["MainRegion"].Deactivate(view);

				this.RegionManager.RequestNavigate("MainRegion", nameof(ToDoDetailView), new NavigationParameters($"id={SelectItem.id}"));

			});
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			Debug.WriteLine("NavigatedTo");
			this.RegionManager = navigationContext.NavigationService.Region.RegionManager;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			Debug.WriteLine("NavigatedFrom");
		}
	}
}