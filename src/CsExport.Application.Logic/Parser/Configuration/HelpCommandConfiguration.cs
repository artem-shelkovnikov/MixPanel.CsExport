using CsExport.Application.Logic.CommandArguments;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class HelpCommandConfiguration : CommandParserConfigurationBase<HelpCommandArguments>
	{
		public override string CommandName => "help";
	}
}