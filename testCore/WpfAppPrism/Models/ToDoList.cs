using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using testCoreClassLibraryStandard;
using testCoreWpfApp;

namespace WpfAppPrism.Models
{
	public class ToDoList
	{
		private DataTable _viewData = new DataTable();

		public ToDoList(DataGrid dataGrid)
		{
			GetUserList(dataGrid);
		}

		#region API処理

		public async void GetUserList( DataGrid dataGrid)
		{
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var res = await hc.GetAsync(common.GetURL() + "people");
			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;

			var js = new Newtonsoft.Json.JsonSerializer();
			var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
			var items = js.Deserialize<List<PersonView>>(jr);
			dataGrid.ItemsSource = items;

		}

		public async void GetById(int id, DataGrid dataGrid)
		{
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var res = await hc.GetAsync(common.GetURL() + "people/" + id);
			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;

			var js = new Newtonsoft.Json.JsonSerializer();
			var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
			var item = js.Deserialize<PersonView>(jr);
			List<PersonView> items = new List<PersonView>();
			items.Add(item);
			dataGrid.ItemsSource = items;
		}

		/// <summary>
		/// 指定の物を追加or更新する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void PutById(PersonView person)
		{
			person.name = "更新データ";
			person.age++;
			var js = new Newtonsoft.Json.JsonSerializer();
			var sw = new System.IO.StringWriter();
			js.Serialize(sw, person);
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = sw.ToString();
			var cont = new StringContent(json, Encoding.UTF8, "application/json");
			var res = await hc.PutAsync(common.GetURL() + "people/" + person.id, cont);
			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;

		}

		/// <summary>
		/// 指定の物を追加する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async Task<PersonView> Post(PersonView person, int maxid)
		{
			//最新IDにする
			person.id = maxid;

			var js = new Newtonsoft.Json.JsonSerializer();
			var sw = new System.IO.StringWriter();
			js.Serialize(sw, person);
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = sw.ToString();
			var cont = new StringContent(json, Encoding.UTF8, "application/json");
			var res = await hc.PostAsync(common.GetURL() + "people", cont);
			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;

			var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
			var item = js.Deserialize<PersonView>(jr);
			return item;
		}

		public async void DeleteById(int id)
		{
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var res = await hc.DeleteAsync(common.GetURL() + "people/" + id);
			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;
		}

		#endregion

	}
}