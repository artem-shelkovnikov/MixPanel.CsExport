namespace CsExport.Core.Settings
{
	public class ClientConfiguration
	{
		private string _secret;
		public string Secret => _secret;

		public string ExportPath { get; }

		public ClientConfiguration()
		{
			ExportPath = ".";
		}

		public void UpdateCredentials(string secret)
		{
			_secret = secret;
		}
	}
}