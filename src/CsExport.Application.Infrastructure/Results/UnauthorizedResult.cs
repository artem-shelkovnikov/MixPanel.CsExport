using CsExport.Application.Infrastructure.IO;

namespace CsExport.Application.Infrastructure.Results
{
	public class UnauthorizedResult : CommandResult
	{
		public override void Handle(IOutput output)
		{
			output.Notify(
				"Unable to authorize to MixPanel. Please check that you called set-credentials command with valid project secret prior to calling current command.");
		}
	}
}