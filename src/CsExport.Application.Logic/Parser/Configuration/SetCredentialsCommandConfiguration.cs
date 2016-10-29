using CsExport.Application.Logic.CommandArguments;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public class SetCredentialsCommandConfiguration : CommandConfiguration<SetCredentialsCommandArguments>
	{
		public SetCredentialsCommandConfiguration()
		{
			HasSignature("set-credentials");

			HasDescription(
				"Sets credentials for all further requests made to MixPanel. Setting credentials is required to be done before any other export command execution.");

			HasArgument(x => x.Secret)
				.WithDescription("MixPanel application secret");
		}
	}
}