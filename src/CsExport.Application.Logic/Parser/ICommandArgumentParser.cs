namespace CsExport.Application.Logic.Parser
{
	public interface ICommandArgumentParser
	{
		bool CanParse(string commandText, CommandDefinition commandDefinition);
		IArguments Parse(string commandText, CommandDefinition commandDefinition);
	}
}