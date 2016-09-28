using System;
using System.Collections.Generic;
using CsExport.Core.Exceptions;
using CsExport.Core.Settings;

namespace CsExport.Core.Client
{
	public class MixPanelClient : IMixPanelClient
	{
		private readonly IWebClient _webClient;
																	  
		private readonly ISigCalculator _sigCalculator;
		private readonly IMixPanelEndpointConfiguration _mixPanelEndpointConfiguration;

		public MixPanelClient(IWebClient webClient,
		 IMixPanelEndpointConfiguration mixPanelEndpointConfiguration, 
		 ISigCalculator sigCalculator)
		{
			_webClient = webClient;
			_mixPanelEndpointConfiguration = mixPanelEndpointConfiguration;	  
			_sigCalculator = sigCalculator;
		}

		public string ExportRaw(IClientConfiguration clientConfiguration, Date from, Date to)
		{
			try
			{
				var uri = _mixPanelEndpointConfiguration.RawExportUri;

				var apiKey = clientConfiguration.ApiKey;

				if (string.IsNullOrWhiteSpace(apiKey))
					throw new ArgumentNullException(_mixPanelEndpointConfiguration.ApiKeyParamName, "API key is not provided for client.");

				var parameterDictionary = new Dictionary<string, string>();

				parameterDictionary.Add(_mixPanelEndpointConfiguration.ApiKeyParamName, apiKey);
				parameterDictionary.Add(_mixPanelEndpointConfiguration.FromDateParamName, from.ToString());
				parameterDictionary.Add(_mixPanelEndpointConfiguration.ToDateParamName, to.ToString());

				var sig = _sigCalculator.Calculate(parameterDictionary, clientConfiguration.Secret);

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