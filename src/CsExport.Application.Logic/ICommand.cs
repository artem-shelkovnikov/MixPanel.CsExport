using CsExport.Core.Settings;

namespace CsExport.Application.Logic
{
	public interface ICommand
	{
		CommandResult Execute();
	}
}