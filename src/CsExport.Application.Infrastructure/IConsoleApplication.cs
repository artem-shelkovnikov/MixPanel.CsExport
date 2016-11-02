namespace CsExport.Application.Infrastructure
{
	public interface IConsoleApplication
	{
		bool IsTerminated();
		void ReadCommand();
	}
}