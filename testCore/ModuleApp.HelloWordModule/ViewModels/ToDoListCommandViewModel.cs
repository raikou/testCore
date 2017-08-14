using System.Data;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Mvvm;
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

		private DataGrid mainGrid;
		public DataGrid MainGrid
		{
			get { return this.mainGrid; }
			set { this.SetProperty(ref this.mainGrid, value); }
		}


		#endregion
		public ToDoListCommandViewModel()
		{
			//コマンド生成
			this.GetMainListCommand = new DelegateCommand(() =>
			{
				mainGrid.ItemsSource = toDoList.GetUserList().Result;
			});
		}


	}
}