using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using testCoreClassLibraryStandard;
using testCoreWpfApp;
using testModuleAppPrism.Views;

namespace testModuleAppPrism.Models
{
	public class ToDoList
	{
		#region API処理

		public async Task<List<TodoDetailData>> GetUserList( )
		{
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var res = await hc.GetAsync(common.GetURL() + "Data").ConfigureAwait(false);
			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;

			var js = new Newtonsoft.Json.JsonSerializer();
			var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
			var items = js.Deserialize<List<TodoDetailData>>(jr);
			return items.ToList();
		}

		public async Task<TodoDetailData> GetById(int id)
		{
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			//var res = await hc.GetAsync(common.GetURL() + "people/" + userid).ConfigureAwait(false); ;
			var res = await hc.GetAsync(common.GetURL() + "data/" + id).ConfigureAwait(false); ;

			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;

			var js = new Newtonsoft.Json.JsonSerializer();
			var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
			var item = js.Deserialize<TodoDetailData>(jr);
			return item;
		}

		/// <summary>
		/// 指定の物を追加or更新する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void PutById(TodoDetailData data)
		{
			data.title = "更新データ";
			data.detail= "適当";
			var js = new Newtonsoft.Json.JsonSerializer();
			var sw = new System.IO.StringWriter();
			js.Serialize(sw, data);
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = sw.ToString();
			var cont = new StringContent(json, Encoding.UTF8, "application/json");
			var res = await hc.PutAsync(common.GetURL() + "data/" + data.userid + "/" + data.dataid, cont).ConfigureAwait(false); ;
			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;

		}

		/// <summary>
		/// 指定の物を追加する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async Task<TodoDetailData> Post(TodoDetailData data, List<TodoDetailData> gridData)
		{
			//最新IDにする
			data.dataid = maxId(gridData);

			var js = new Newtonsoft.Json.JsonSerializer();
			var sw = new System.IO.StringWriter();
			js.Serialize(sw, data);
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = sw.ToString();
			var cont = new StringContent(json, Encoding.UTF8, "application/json");
			var res = await hc.PostAsync(common.GetURL() + "data", cont).ConfigureAwait(false); ;
			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;

			var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(str));
			var item = js.Deserialize<TodoDetailData>(jr);
			return item;
		}

		public async void DeleteById(int userid, int dataid)
		{
			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var res = await hc.DeleteAsync(common.GetURL() + "data/" + userid + "/" + dataid).ConfigureAwait(false); ;
			var str = await res.Content.ReadAsStringAsync();
			//testMess.Text = str;
		}

		/// <summary>
		/// 最大番号を取得する
		/// </summary>
		/// <returns></returns>
		private int maxId(List<TodoDetailData> gridData)
		{
			int result = gridData.Max(x => x.dataid);
			return result + 1;
		}

		#endregion

	}
}