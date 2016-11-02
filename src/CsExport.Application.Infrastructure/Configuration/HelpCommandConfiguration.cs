using CsExport.Application.Infrastructure.CommandArguments;
using CsExport.Application.Infrastructure.Parser;

namespace CsExport.Application.Infrastructure.Configuration
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