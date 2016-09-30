using System;

namespace CsExport.Core.Client
{
	public interface IWebClient
	{
		string QueryUri(Uri baseUri, BasicAuthentication basicAuth);
	}
}