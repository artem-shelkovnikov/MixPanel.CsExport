using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Commands;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class SetCredentialsCommandConfiguration	: CommandParserConfigurationBase<SetCredentialsCommandArguments>
	{
		public override string CommandName { get { return "set-credentials"; } } 
	}
}