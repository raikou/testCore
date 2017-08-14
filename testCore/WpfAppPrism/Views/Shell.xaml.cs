using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using testCoreClassLibraryStandard;
using WpfAppPrism.Models;

namespace WpfAppPrism.Views
{
	/// <summary>
	/// Shell.xaml の相互作用ロジック
	/// </summary>
	public partial class Shell : Window
	{
		private ToDoList toDoList;
		public Shell()
		{
			InitializeComponent();
			toDoList = new ToDoList(ViewGrid);
		}
		#region ユーザー処理

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var person = (PersonView)ViewGrid.SelectedItem;

			toDoList.PutById(person);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			PersonView person = new PersonView();
			person.name = "新規データ";
			person.age = 0;
			toDoList.Post(person, maxId());

			//最新情報取得
			toDoList.GetUserList(ViewGrid);
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			var person = (PersonView)ViewGrid.SelectedItem;
			toDoList.DeleteById(person.id);

			//最新情報取得
			toDoList.GetUserList(ViewGrid);
		}

		#endregion
		#region 内部処理

		/// <summary>
		/// 最大番号を取得する
		/// </summary>
		/// <returns></returns>
		private int maxId()
		{
			int result = ((List<PersonView>)ViewGrid.ItemsSource).Max(x => x.id);
			return result + 1;
		}

		#endregion

	}
}
