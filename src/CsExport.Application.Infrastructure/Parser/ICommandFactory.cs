namespace CsExport.Application.Infrastructure.Parser
{
	public interface ICommandFactory
	{
		ICommand Create(IArguments arguments);
	}
}