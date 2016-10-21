namespace CsExport.Core.Settings
{
	public class ClientConfiguration
	{							 
		private string _secret;								
		public string Secret => _secret;

		public void UpdateCredentials(string secret)
		{						
			_secret = secret;
		}
	}
}