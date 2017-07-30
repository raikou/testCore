using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using testCoreClassLibraryStandard;

using Newtonsoft.Json;

namespace testCoreWpfApp
{
	//http://www.moonmile.net/blog/archives/7971
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private DataTable _viewData = new DataTable();

		public MainWindow()
		{
			InitializeComponent();

			ViewData();

		}

		#region HttpClientに置き換えてみる

		//private void ViewData()
		//{
		//	HttpWebRequest req = common.connectionGet("people");

		//	HttpWebResponse res = (HttpWebResponse)req.GetResponse();
		//	List<PersonView> result;
		//	using (res)
		//	{
		//		using (var resStream = res.GetResponseStream())
		//		{
		//			StreamReader sr = new StreamReader(resStream);
		//			string str = sr.ReadToEnd();
		//			result = JsonConvert.DeserializeObject<List<PersonView>>(str.ToString());
		//		}
		//	}
		//	ViewGrid.DataContext = common.ConvertToDataTable(result);

		//}

		//private void PostData(PersonView data)
		//{
		//	HttpWebRequest req = common.connectionPost("people");

		//	using (var streamWriter = new StreamWriter(req.GetRequestStream()))
		//	{
		//		string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
		//		streamWriter.Write(json);
		//	}

		//	HttpWebResponse res = (HttpWebResponse)req.GetResponse();
		//	List<PersonView> result = new List<PersonView>();
		//	using (res)
		//	{
		//		using (var resStream = res.GetResponseStream())
		//		{
		//			StreamReader sr = new StreamReader(resStream);
		//			string str = sr.ReadToEnd();
		//			var r = JsonConvert.DeserializeObject<PersonView>(str.ToString());
		//			result.Add(r);
		//		}
		//	}
		//	ViewGrid.DataContext = common.ConvertToDataTable(result);
		//}
		//

		//private void PostDataList(List<PersonView> data)
		//{
		//	HttpWebRequest req = common.connectionPost("people");

		//	using (var streamWriter = new StreamWriter(req.GetRequestStream()))
		//	{
		//		string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
		//		streamWriter.Write(json);
		//	}

		//	HttpWebResponse res = (HttpWebResponse)req.GetResponse();
		//	List<PersonView> result = new List<PersonView>();
		//	using (res)
		//	{
		//		using (var resStream = res.GetResponseStream())
		//		{
		//			StreamReader sr = new StreamReader(resStream);
		//			string str = sr.ReadToEnd();
		//			var r = JsonConvert.DeserializeObject<PersonView>(str.ToString());
		//			result.Add(r);
		//		}
		//	}
		//	ViewGrid.DataContext = common.ConvertToDataTable(result);
		//}


		//private void Button_Click(object sender, RoutedEventArgs e)
		//{
		//	DataTable table = ViewGrid.DataContext as DataTable;
		//	DataTable list = table.GetChanges();
		//	foreach (PersonView item in list.Rows)
		//	{
		//		PostData(item);
		//	}
		//}
		#endregion

		private void ViewData()
		{
			Get();
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			PutById();
		}

		private async void Get()
		{
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var res = await hc.GetAsync(common.GetURL() + "people");
			var str = await res.Content.ReadAsStringAsync();
			testMess.Text = str;

			var js = new Newtonsoft.Json.JsonSerializer();
			var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
			var items = js.Deserialize<List<PersonView>>(jr);
			ViewGrid.ItemsSource = items;

		}

		private async void GetById(int id)
		{
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var res = await hc.GetAsync(common.GetURL() + "people/" + id);
			var str = await res.Content.ReadAsStringAsync();
			testMess.Text = str;

			var js = new Newtonsoft.Json.JsonSerializer();
			var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
			var item = js.Deserialize<PersonView>(jr);
			List<PersonView> items = new List<PersonView>();
			items.Add(item);
			ViewGrid.ItemsSource = items;
		}

		private int maxId()
		{
			int result = ((List<PersonView>)ViewGrid.ItemsSource).Max(x => x.id);
			return result + 1;
		}

		/// <summary>
		/// 指定の物を追加or更新する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void PutById()
		{
			var person = (PersonView)ViewGrid.SelectedItem;
			person.name = "更新データ";
			person.age++;
			var js = new Newtonsoft.Json.JsonSerializer();
			var sw = new System.IO.StringWriter();
			js.Serialize(sw, person);
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = sw.ToString();
			var cont = new StringContent(json, Encoding.UTF8, "application/json");
			var res = await hc.PutAsync(common.GetURL() + "people/" + person.id , cont);
			var str = await res.Content.ReadAsStringAsync();
			testMess.Text = str;

		}

		/// <summary>
		/// 指定の物を追加する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async Task<PersonView> Post(PersonView person)
		{
			//最新IDにする
			person.id = maxId();

			var js = new Newtonsoft.Json.JsonSerializer();
			var sw = new System.IO.StringWriter();
			js.Serialize(sw, person);
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = sw.ToString();
			var cont = new StringContent(json, Encoding.UTF8, "application/json");
			var res = await hc.PostAsync(common.GetURL() + "people", cont);
			var str = await res.Content.ReadAsStringAsync();
			testMess.Text = str;

			var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
			var item = js.Deserialize<PersonView>(jr);
			return item;
		}

		private async void DeleteById(int id)
		{
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var res = await hc.DeleteAsync(common.GetURL() + "people/" + id);
			var str = await res.Content.ReadAsStringAsync();
			testMess.Text = str;
		}

		#region 多分要らない
		//private async void Create(object sender, RoutedEventArgs e)
		//{
		//	var person = new PersonView() { id = maxId(), name = "new person", age = 0 };
		//	var js = new Newtonsoft.Json.JsonSerializer();
		//	var sw = new System.IO.StringWriter();
		//	js.Serialize(sw, person);
		//	var hc = new HttpClient();
		//	hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		//	var json = sw.ToString();
		//	var cont = new StringContent(json, Encoding.UTF8, "application/json");
		//	var res = await hc.PostAsync(common.GetURL() + "people/Create", cont);
		//	var str = await res.Content.ReadAsStringAsync();
		//	testMess.Text = str;

		//	var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
		//	var item = js.Deserialize<PersonView>(jr);
		//	List<PersonView> items = new List<PersonView>();
		//	items.Add(item);
		//	ViewGrid.ItemsSource = items;
		//}

		//private async void Edit(object sender, RoutedEventArgs e)
		//{
		//	var person = (PersonView)ViewGrid.SelectedItem;
		//	var js = new Newtonsoft.Json.JsonSerializer();
		//	var sw = new System.IO.StringWriter();
		//	js.Serialize(sw, person);
		//	var hc = new HttpClient();
		//	hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		//	var json = sw.ToString();
		//	var cont = new StringContent(json, Encoding.UTF8, "application/json");
		//	var res = await hc.PostAsync(common.GetURL() + "people/Edit/{id}", cont);
		//	var str = await res.Content.ReadAsStringAsync();
		//	testMess.Text = str;

		//	var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
		//	var item = js.Deserialize<PersonView>(jr);
		//	List<PersonView> items = new List<PersonView>();
		//	items.Add(item);
		//	ViewGrid.ItemsSource = items;
		//}
		#endregion

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			PersonView person= new	PersonView();
			person.name = "新規データ";
			person.age = 0;
			Post(person);
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			var person = (PersonView)ViewGrid.SelectedItem;
			DeleteById(person.id);
		}
	}
}
