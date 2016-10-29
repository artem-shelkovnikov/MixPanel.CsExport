namespace CsExport.Application.Logic.Parser
{
	public interface ICommandFactory
	{
		ICommand Create(IArguments arguments);
	}
}