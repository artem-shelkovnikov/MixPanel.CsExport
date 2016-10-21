using System.Collections.Generic;

namespace CsExport.Application.Logic.Parser.Utility
{
	public class ConsoleCommandDefinition
	{
		public string Name { get; set; }
		public IEnumerable<CommandArgument> Arguments { get; set; }
	}
}