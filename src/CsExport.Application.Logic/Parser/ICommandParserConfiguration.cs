using System.Collections.Generic;
using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Parser
{
	public interface ICommandParserConfiguration
	{
		string CommandName { get; }

		IArguments TryParse(IEnumerable<CommandArgument> arguments);
	}
}