using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using Prism.Commands;
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
		#region コマンドリスト
		public DelegateCommand GetMainListCommand { get; }
		#endregion

		#region コマンドで利用する画面情報

		private DataGrid mainGrid = new DataGrid();
		public DataGrid MainGrid
		{
			get { return this.mainGrid; }
			set { this.SetProperty(ref this.mainGrid, value); }
		}


		#endregion
		public ToDoListCommandViewModel()
		{
			DataGrid ttt = new DataGrid();
			ttt.ItemsSource = new List<PersonView>()
			{
				new PersonView()
				{
					id = 0,
					name = "aaa",
					age = 1
				}
			};
			mainGrid = ttt;
			//コマンド生成
			this.GetMainListCommand = new DelegateCommand(() =>
			{
				mainGrid.ItemsSource = toDoList.GetUserList().Result;
			});
		}


	}
}