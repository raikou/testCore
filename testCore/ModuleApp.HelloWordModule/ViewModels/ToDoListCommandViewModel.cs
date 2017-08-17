using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using testCoreClassLibraryStandard;
using testModuleAppPrism.Models;

namespace testModuleAppPrism.ViewModels
{
	public class ToDoListCommandViewModel :BindableBase
	{
		public string HeaderText { get; } = "ToDoListCommand";

		#region Model情報
		ToDoList toDoList = new ToDoList();
		#endregion

		#region 通知ダイアログ
		/// <summary>
		/// メッセージ用（OKのみ）
		/// </summary>
		public InteractionRequest<Notification> NotificationRequest { get; } = new InteractionRequest<Notification>();
		#endregion
		#region コマンドリスト
		public DelegateCommand GetMainListCommand { get; }
		public DelegateCommand Add { get; }
		public DelegateCommand Upd { get; }
		public DelegateCommand Del { get; }
		#endregion

		#region コマンドで利用する画面情報

		private List<PersonView> gridItem = new List<PersonView>();
		public List<PersonView> GridItem
		{
			get { return this.gridItem; }
			set { this.SetProperty(ref this.gridItem, value); }
		}


		#endregion
		public ToDoListCommandViewModel()
		{
			gridItem = new List<PersonView>()
			{
				new PersonView()
				{
					id = 0,
					name = "aaa",
					age = 1
				}
			};
			//コマンド生成
			this.GetMainListCommand = new DelegateCommand(() =>
			{
				GridItem = toDoList.GetUserList().Result;
			});
			this.Add = new DelegateCommand(() =>
			{
				PersonView person = new PersonView();
				person.name = "新規データ";
				person.age = 0;
				toDoList.Post(person, GridItem );

				GridItem = toDoList.GetUserList().Result;
			});
			this.Upd = new DelegateCommand(() =>
			{
				this.NotificationRequest.Raise(new Notification { Title = "Alert", Content = "未実装です" });
			});
			this.Del = new DelegateCommand(() =>
			{
				this.NotificationRequest.Raise(new Notification { Title = "Alert", Content = "未実装です" });
			});
		}


	}
}