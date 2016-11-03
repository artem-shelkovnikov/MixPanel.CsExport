namespace CsExport.Application.Infrastructure
{
	public interface ICommandFactory
	{
		ICommand Create(IArguments arguments);
	}
}