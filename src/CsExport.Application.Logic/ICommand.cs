using CsExport.Core.Client;

namespace CsExport.Application.Logic
{
	public interface ICommand
	{
		CommandResult Execute(IMixPanelClient client, IInputProvider inputProvider);
	}
}