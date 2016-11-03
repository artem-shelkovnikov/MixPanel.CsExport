using CsExport.Application.Infrastructure.Builtin.CommandArguments;
using CsExport.Application.Infrastructure.FluentConfiguration;

namespace CsExport.Application.Infrastructure.Builtin.Configuration
{
	internal class HelpCommandConfiguration : CommandConfiguration<HelpCommandArguments>
	{
		public HelpCommandConfiguration()
		{
			HasSignature("help");

			HasDescription("Retrieves list of commands for application");
		}
	}
}