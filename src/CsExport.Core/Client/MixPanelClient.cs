using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CsExport.Core.Exceptions;
using CsExport.Core.Settings;

namespace CsExport.Core.Client
{
	public class MixPanelClient : IMixPanelClient
	{
		private readonly IWebClient _webClient;
		private const string FromDateParamName = "from_date";
		private const string ToDateParamName = "to_date";

		public MixPanelClient(IWebClient webClient)
		{
			_webClient = webClient;											  
		}

		public bool VerifyCredentials(ClientConfiguration clientConfiguration)
		{
			try
			{
				ExportRaw(clientConfiguration, new Date(2000, 1, 1), new Date(2000, 1, 2));
			}
			catch
			{
				return false;
			}
			return true;
		}

		public string ExportRaw(ClientConfiguration clientConfiguration, Date from, Date to)
		{
			try
			{
				var uri = MixPanelEndpointConfiguration.RawExportUrl;

				var parameterDictionary = new Dictionary<string, string>();

				parameterDictionary.Add(FromDateParamName, from.ToString());
				parameterDictionary.Add(ToDateParamName, to.ToString());

				var callingUri =
					new Uri(uri.ToString() + "?" + string.Join("&", parameterDictionary.Select(x => x.Key + "=" + x.Value)));

				var webClientResponse = _webClient.QueryUri(callingUri,
					new BasicAuthentication {UserName = clientConfiguration.Secret});

				return webClientResponse;
			}
			catch (WebException ex)
			{
				TryHandleWebException(ex);
				throw new MixPanelClientException(ex);
			}
			catch (Exception ex)
			{
				throw new MixPanelClientException(ex);
			}
		}

		private static void TryHandleWebException(WebException ex)
		{
			if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
			{
				var webResponse = (HttpWebResponse) ex.Response;
				if (webResponse == null)
				{
					return;
				}

				switch (webResponse.StatusCode)
				{
					case HttpStatusCode.Unauthorized:
						throw new MixPanelUnauthorizedException();
					case HttpStatusCode.BadRequest:
						var responseStream = webResponse.GetResponseStream();
						if (responseStream == null)
							return;

						var responseReader = new StreamReader(responseStream);
						var response = responseReader.ReadToEnd();
						if (response.Contains("Unable to authenticate")) //some magic
							throw new MixPanelUnauthorizedException();
						break;
				}
			}		   
		}
	}
}