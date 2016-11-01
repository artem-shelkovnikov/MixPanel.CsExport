namespace CsExport.Application.Logic
{
	public interface IConsoleApplication
	{
		bool IsTerminated();
		void ReadCommand();
	}
}