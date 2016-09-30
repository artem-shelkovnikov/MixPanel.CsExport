namespace CsExport.Application.Logic.Results
{
	public class SuccessResult : CommandResult
	{
		public override void Handle(IOutput output)
		{
			output.Notify("Done!");
		}
	}
}