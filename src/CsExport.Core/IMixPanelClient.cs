using System.Collections.Generic;

namespace CsExport.Core
{
	public interface IMixPanelClient
	{
		string ExportRaw(Date from, Date to);
	}
}