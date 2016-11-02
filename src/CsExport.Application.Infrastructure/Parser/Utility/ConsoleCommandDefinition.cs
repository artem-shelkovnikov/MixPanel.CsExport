using System.Collections.Generic;

namespace CsExport.Application.Infrastructure.Parser.Utility
{
	public class ConsoleCommandDefinition
	{
		public string Name { get; set; }
		public IEnumerable<CommandArgument> Arguments { get; set; }
	}
}