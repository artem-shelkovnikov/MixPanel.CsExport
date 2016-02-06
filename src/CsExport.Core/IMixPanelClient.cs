using System.Collections.Generic;

namespace CsExport.Core
{
	public interface IMixPanelClient
	{
		IEnumerable<ExportResult> ExportRaw();
	}

	public class ExportResult
	{
	}
}