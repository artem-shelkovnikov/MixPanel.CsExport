namespace CsExport.Application.Logic.Parser
{
	public interface ICommandParser
	{
		ICommand ParseCommand(string commandText);
	}
}