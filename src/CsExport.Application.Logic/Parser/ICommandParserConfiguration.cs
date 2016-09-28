namespace CsExport.Application.Logic.Parser
{
	public interface ICommandParserConfiguration
	{
		ICommand TryParse(string input);
	}
}