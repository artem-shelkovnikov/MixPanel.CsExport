using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic
{
	public interface ICommand
	{
		CommandResult Execute(ExecutionSettings settings);
	}

	public class ExecutionSettings
	{
		 public IClientConfiguration ClientConfiguration { get; set; }
		 
		 public IMixPanelClient MixPanelClient { get; set; }
		 public IInputProvider InputProvider { get; set; }
	}
}