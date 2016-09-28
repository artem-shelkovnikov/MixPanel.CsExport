using CsExport.Core.Settings;

namespace CsExport.Core.Client
{
	public interface IMixPanelClient
	{
		string ExportRaw(IClientConfiguration clientConfiguration, Date from, Date to);
	}
}