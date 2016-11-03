using CsExport.Application.Infrastructure.FluentConfiguration;

namespace CsExport.Application.Infrastructure.Parser
{
	public interface ICommandArgumentParser
	{
		bool CanParse(string commandText, CommandDefinition commandDefinition);
		IArguments Parse(string commandText, CommandDefinition commandDefinition);
	}
}