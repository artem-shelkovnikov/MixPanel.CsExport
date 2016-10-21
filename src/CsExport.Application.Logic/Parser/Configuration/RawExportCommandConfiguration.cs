using CsExport.Application.Logic.CommandArguments;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class RawExportCommandConfiguration	: CommandParserConfigurationBase<RawExportCommandArguments>
	{
		public override string CommandName { get { return "raw-export"; } }		   
	}
}