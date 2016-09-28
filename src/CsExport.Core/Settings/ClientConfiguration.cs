namespace CsExport.Core.Settings
{
	public class ClientConfiguration
	{
		private string _apiKey;
		private string _secret;
		public string ApiKey { get { return _apiKey; } }
		public string Secret { get { return _secret; } }

		public void UpdateCredentials(string apiKey, string secret)
		{
			_apiKey = apiKey;
			_secret = secret;
		}
	}
}