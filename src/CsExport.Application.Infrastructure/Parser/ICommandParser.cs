namespace CsExport.Application.Infrastructure.Parser
{
	public interface ICommandParser
	{
		ICommand ParseCommand(string commandText);
	}
}