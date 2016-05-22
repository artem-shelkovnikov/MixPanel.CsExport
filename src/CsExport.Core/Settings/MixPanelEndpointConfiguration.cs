using System;

namespace CsExport.Core.Settings
{
	public class MixPanelEndpointConfiguration : IMixPanelEndpointConfiguration
	{
		public Uri RawExportUri { get { return new Uri("https://data.mixpanel.com/api/2.0/export/"); } }
		public string SigParamName { get { return "sig"; } }
		public string ApiKeyParamName { get { return "apiKey"; } }
		public string FromDateParamName { get { return "from_date"; } }
		public string ToDateParamName { get { return "to_date"; } }
	}
}