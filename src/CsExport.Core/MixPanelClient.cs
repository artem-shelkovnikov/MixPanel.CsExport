using System;
using System.Collections.Generic;
using CsExport.Core.Exceptions;

namespace CsExport.Core
{
	public class MixPanelClient : IMixPanelClient
	{
		private readonly IWebClient _webClient;
		private readonly IJsonSerializer _jsonSerializer;
		private readonly IMixPanelEndpointConfiguration _mixPanelEndpointConfiguration;

		public MixPanelClient(IWebClient webClient, IJsonSerializer jsonSerializer, IMixPanelEndpointConfiguration mixPanelEndpointConfiguration)
		{
			_webClient = webClient;
			_jsonSerializer = jsonSerializer;
			_mixPanelEndpointConfiguration = mixPanelEndpointConfiguration;
		}

		public IEnumerable<ExportResult> ExportRaw()
		{
			try
			{
				var uri = _mixPanelEndpointConfiguration.RawExportUri;
				var webClientResponse = _webClient.QueryUri(uri);

				var deserializedData = _jsonSerializer.Deserialize<IEnumerable<ExportResult>>(webClientResponse);

				return deserializedData;
			}
			catch (Exception ex)
			{
				throw new MixPanelClientException(ex);
			}
		}
	}
}