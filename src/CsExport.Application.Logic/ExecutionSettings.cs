using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic
{
	public class ExecutionSettings
	{
		public ClientConfiguration ClientConfiguration { get; set; }
		 
		public IMixPanelClient MixPanelClient { get; set; }
		public IInput Input { get; set; }
	}
}