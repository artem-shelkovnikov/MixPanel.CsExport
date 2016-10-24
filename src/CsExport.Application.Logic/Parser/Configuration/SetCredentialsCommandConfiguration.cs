using CsExport.Application.Logic.CommandArguments;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class SetCredentialsCommandConfiguration : CommandParserConfigurationBase<SetCredentialsCommandArguments>
	{
		public override string CommandName => "set-credentials";
	}
}