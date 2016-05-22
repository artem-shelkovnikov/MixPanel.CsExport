using System;
using System.Collections.Generic;
using CsExport.Core.Exceptions;
using CsExport.Core.Settings;

namespace CsExport.Core.Client
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
					throw new ArgumentNullException(_mixPanelEndpointConfiguration.ApiKeyParamName, "API key is not provided for client.");

				var parameterDictionary = new Dictionary<string, string>();

				parameterDictionary.Add(_mixPanelEndpointConfiguration.ApiKeyParamName, apiKey);
				parameterDictionary.Add(_mixPanelEndpointConfiguration.FromDateParamName, from.ToString());
				parameterDictionary.Add(_mixPanelEndpointConfiguration.ToDateParamName, to.ToString());

				var sig = _sigCalculator.Calculate(parameterDictionary, _clientConfiguration.Secret);

				parameterDictionary.Add(_mixPanelEndpointConfiguration.SigParamName, sig);

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