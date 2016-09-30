using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic
{
	public class ExecutionSettings
	{
		public ApplicationConfiguration ApplicationConfiguration { get; set; }
		public ClientConfiguration ClientConfiguration { get; set; }
		 
		public IMixPanelClient MixPanelClient { get; set; }
		public IInput Input { get; set; }
		public IFileWriter FileWriter { get; set; }
	}

	public class ApplicationConfiguration
	{										  
		public string ExportPath { get; set; }
	}
}