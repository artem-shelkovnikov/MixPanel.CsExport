using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Commands;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class HelpCommandConfiguration : CommandParserConfigurationBase<HelpCommandArguments>
	{
		public override string CommandName { get { return "help"; } }	 
	}
}