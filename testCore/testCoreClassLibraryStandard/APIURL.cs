namespace testCoreClassLibraryStandard
{
	public static class APIURL
	{
		private const string urlMain = "http://192.168.52.128/api/";
		private const string urlLocal = "http://localhost:55192/api/";
		public static string URL
		{
			get
			{
				return urlMain;
			}
		}
	}
}