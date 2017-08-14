using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace testCoreWpfApp
{
	public static class common
	{
		public static string GetURL()
		{
			return WpfAppPrism.Properties.Resources.connectionApiURL;
			//return WpfAppPrism.Properties.Resources.connectionApiURLLocal;
		}

		public static HttpWebRequest connectionGet(string url)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GetURL() + url);

			req.ContentType = "application/json";
			req.Method = "GET";

			return req;
		}

		public static HttpWebRequest connectionPut(string url)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GetURL() + url);

			req.ContentType = "application/json";
			req.Method = "PUT";

			return req;
		}

		public static HttpWebRequest connectionPost(string url)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GetURL() + url);

			req.ContentType = "application/json";
			req.Method = "POST";

			return req;
		}

		public static HttpWebRequest connectionDelete(string url)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GetURL() + url);

			req.ContentType = "application/json";
			req.Method = "DELETE";

			return req;
		}

		public static DataTable ConvertToDataTable<T>(T list) where T : IList
		{
			var table = new DataTable(typeof(T).GetGenericArguments()[0].Name);

			table.BeginLoadData();

			typeof(T).GetGenericArguments()[0].GetProperties().
				ToList().ForEach(p => table.Columns.Add(p.Name, p.PropertyType));
			foreach (var item in list)
			{
				var row = table.NewRow();
				item.GetType().GetProperties().
					ToList().ForEach(p => row[p.Name] = p.GetValue(item, null));
				table.Rows.Add(row);
			}

			table.EndLoadData();

			return table;
		}
	}
}
