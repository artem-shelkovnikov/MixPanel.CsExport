using System.Collections.Generic;

namespace CsExport.Core
{
	public interface ISigCalculator
	{
		string Calculate(IDictionary<string, string> parameters, string secret);
	}
}