namespace CsExport.Application.Infrastructure
{
	public interface IResultHandler
	{
		void HandleResult(CommandResult commandResult);
	}
}