using CsExport.Application.Logic.IO;

namespace CsExport.Application.Logic.Results
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