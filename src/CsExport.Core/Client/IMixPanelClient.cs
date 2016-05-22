namespace CsExport.Core.Client
{
	public interface IMixPanelClient
	{
		string ExportRaw(Date from, Date to);
	}
}