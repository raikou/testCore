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
			url = "http://192.168.52.128/api/people";
			//url = "http://localhost:55192/api/people";
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

			req.ContentType = "application/json";
			req.Method = "GET";


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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ViewData();
		}
	}
}
