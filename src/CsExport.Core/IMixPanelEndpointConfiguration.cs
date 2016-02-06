using System;

namespace CsExport.Core
{
	public interface IMixPanelEndpointConfiguration
	{
		Uri RawExportUri { get; set; }
	}
}