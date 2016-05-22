using System;

namespace CsExport.Core.Settings
{
	public interface IMixPanelEndpointConfiguration
	{
		Uri RawExportUri { get; }
		string SigParamName { get; }
		string ApiKeyParamName { get; }
		string FromDateParamName { get; }
		string ToDateParamName { get; }
	}
}