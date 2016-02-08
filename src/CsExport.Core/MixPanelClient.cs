using System;
using System.Collections.Generic;
using CsExport.Core.Exceptions;

namespace CsExport.Core
{
	public class MixPanelClient : IMixPanelClient
	{
		private readonly IWebClient _webClient;

		private readonly IClientConfiguration _clientConfiguration;
		private readonly ISigCalculator _sigCalculator;
		private readonly IMixPanelEndpointConfiguration _mixPanelEndpointConfiguration;

		public MixPanelClient(IWebClient webClient,
		 IMixPanelEndpointConfiguration mixPanelEndpointConfiguration, 
		 IClientConfiguration clientConfiguration,
		 ISigCalculator sigCalculator)
		{
			_webClient = webClient;
			_mixPanelEndpointConfiguration = mixPanelEndpointConfiguration;
			_clientConfiguration = clientConfiguration;
			_sigCalculator = sigCalculator;
		}

		public string ExportRaw(Date from, Date to)
		{
			try
			{
				var uri = _mixPanelEndpointConfiguration.RawExportUri;

				var apiKey = _clientConfiguration.ApiKey;

				if (string.IsNullOrWhiteSpace(apiKey))
					throw new ArgumentNullException("apiKey", "API key is not provided for client.");

				var parameterDictionary = new Dictionary<string, string>();

				parameterDictionary.Add("apiKey", apiKey);
				parameterDictionary.Add("from_date", from.ToString());
				parameterDictionary.Add("to_date", to.ToString());

				var sig = _sigCalculator.Calculate(parameterDictionary, _clientConfiguration.Secret);

				parameterDictionary.Add("sig", sig);

				var webClientResponse = _webClient.QueryUri(uri, parameterDictionary);												  

				return webClientResponse;
			}
			catch (Exception ex)
			{
				throw new MixPanelClientException(ex);
			}
		}
	}
}