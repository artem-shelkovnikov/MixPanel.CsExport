using System;
using System.Net;

namespace CsExport.Core.Client
{
	public class DefaultWebClient : IWebClient
	{
		private readonly WebClient _webClient = new WebClient();
		public string QueryUri(Uri baseUri, BasicAuthentication authentication)
		{
			var basicAuthToken =
				System.Convert.ToBase64String(
					System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(authentication.UserName + ":" + authentication.Password));

			_webClient.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", basicAuthToken);
			return _webClient.DownloadString(baseUri);
		}
	}
}