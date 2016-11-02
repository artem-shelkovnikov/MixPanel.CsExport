using CsExport.Application.Infrastructure.IO;

namespace CsExport.Application.Infrastructure.Results
{
	public class SuccessResult : CommandResult
	{
		public override void Handle(IOutput output)
		{
			output.Notify("Done!");
		}
	}
}