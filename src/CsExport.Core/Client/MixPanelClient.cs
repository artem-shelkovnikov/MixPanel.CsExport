using System;
using System.Collections.Generic;
using System.Linq;
using CsExport.Core.Exceptions;
using CsExport.Core.Settings;

namespace CsExport.Core.Client
{
	public class MixPanelClient : IMixPanelClient
	{
		private readonly IWebClient _webClient;
																	  
		private readonly IMixPanelEndpointConfiguration _mixPanelEndpointConfiguration;

		public MixPanelClient(IWebClient webClient,
		 IMixPanelEndpointConfiguration mixPanelEndpointConfiguration)
		{
			_webClient = webClient;
			_mixPanelEndpointConfiguration = mixPanelEndpointConfiguration;	
		}

		public string ExportRaw(ClientConfiguration clientConfiguration, Date from, Date to)
		{
			try
			{
				var uri = _mixPanelEndpointConfiguration.RawExportUri;																	   

				var parameterDictionary = new Dictionary<string, string>();
																										 
				parameterDictionary.Add(_mixPanelEndpointConfiguration.FromDateParamName, from.ToString());
				parameterDictionary.Add(_mixPanelEndpointConfiguration.ToDateParamName, to.ToString());	 
				
				var callingUri = new Uri(uri.ToString() + "?" + string.Join("&", parameterDictionary.Select(x => x.Key + "=" + x.Value)));

				var webClientResponse = _webClient.QueryUri(callingUri, new BasicAuthentication {UserName = clientConfiguration.Secret});												  

				return webClientResponse;
			}
			catch (Exception ex)
			{
				throw new MixPanelClientException(ex);
			}
		}
	}
}