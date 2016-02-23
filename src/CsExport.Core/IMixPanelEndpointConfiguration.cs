using System;

namespace CsExport.Core
{
	public interface IMixPanelEndpointConfiguration
	{
		Uri RawExportUri { get; set; }
		string SigParamName { get; set; }
		string ApiKeyParamName { get; set; }
		string FromDateParamName { get; set; }
		string ToDateParamName { get; set; }
	}
}