using CsExport.Core.Settings;

namespace CsExport.Core.Client
{
	public interface IMixPanelClient
	{
		bool VerifyCredentials(ClientConfiguration clientConfiguration);
		string ExportRaw(ClientConfiguration clientConfiguration, Date from, Date to, string[] events = null);
	}
}