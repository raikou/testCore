using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			ViewData();

		}

		private void ViewData()
		{
			string url;
			HttpWebRequest req = common.connectionGet("people");

			HttpWebResponse res = (HttpWebResponse)req.GetResponse();
			List<PersonView> result;
			using (res)
			{
				using (var resStream = res.GetResponseStream())
				{
					StreamReader sr = new StreamReader(resStream);
					string str = sr.ReadToEnd();
					result = JsonConvert.DeserializeObject<List<PersonView>>(str.ToString());
				}
			}

			DataGrid.ItemsSource = result;
		}

		private void PostData()
		{
			HttpWebRequest req = common.connectionPost("people");

			using (var streamWriter = new StreamWriter(req.GetRequestStream()))
			{
				var data = new PersonView()
				{
					id = 100,
					name = "ポスト",
					age = 21
				};
				string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
				streamWriter.Write(json);
			}

			HttpWebResponse res = (HttpWebResponse)req.GetResponse();
			List<PersonView> result = new List<PersonView>();
			using (res)
			{
				using (var resStream = res.GetResponseStream())
				{
					StreamReader sr = new StreamReader(resStream);
					string str = sr.ReadToEnd();
					var r = JsonConvert.DeserializeObject<PersonView>(str.ToString());
					result.Add(r);
				}
			}
			DataGrid.ItemsSource = result;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			PostData();
		}

	}
}
