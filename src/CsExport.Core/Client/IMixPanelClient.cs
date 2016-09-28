using CsExport.Core.Settings;

namespace CsExport.Core.Client
{
	public interface IMixPanelClient
	{
		string ExportRaw(ClientConfiguration clientConfiguration, Date from, Date to);
	}
}