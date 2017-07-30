using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace testCoreWpfApp
{
	public static class common
	{
		public static string GetURL()
		{
			//return Properties.Resources.connectionApiURL;
			return Properties.Resources.connectionApiURLLocal;
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

	}
}
