using System;
using CsExport.Core.Exceptions;

namespace CsExport.Core
{
	public class MixPanelClient : IMixPanelClient
	{
		private readonly IWebClient _webClient;
		private readonly IMixPanelEndpointConfiguration _mixPanelEndpointConfiguration;

		public MixPanelClient(IWebClient webClient, IMixPanelEndpointConfiguration mixPanelEndpointConfiguration)
		{
			_webClient = webClient;
			_mixPanelEndpointConfiguration = mixPanelEndpointConfiguration;
		}

		public string ExportRaw()
		{
			try
			{
				var uri = _mixPanelEndpointConfiguration.RawExportUri;

				var webClientResponse = _webClient.QueryUri(uri);												  

				return webClientResponse;
			}
			catch (Exception ex)
			{
				throw new MixPanelClientException(ex);
			}
		}
	}
}