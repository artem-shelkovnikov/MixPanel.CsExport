using System;

namespace CsExport.Core
{
	public interface IWebClient
	{
		string QueryUri(Uri uri);
	}
}