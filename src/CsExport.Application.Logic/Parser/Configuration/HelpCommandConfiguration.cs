using CsExport.Application.Logic.CommandArguments;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class HelpCommandConfiguration : CommandConfiguration<HelpCommandArguments>
	{
		public HelpCommandConfiguration()
		{
			HasSignature("help");

			HasDescription("Retrieves list of commands for application");
		}
	}
}