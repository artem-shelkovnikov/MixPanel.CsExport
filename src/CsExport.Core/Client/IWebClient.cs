using System;
using System.Collections.Generic;

namespace CsExport.Core.Client
{
	public interface IWebClient
	{
		string QueryUri(Uri uri, IDictionary<string, string> queryParametersDictionary);
	}
}