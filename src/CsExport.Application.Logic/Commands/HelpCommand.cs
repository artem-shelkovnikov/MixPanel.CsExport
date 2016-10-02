using CsExport.Application.Logic.Results;

namespace CsExport.Application.Logic.Commands
{
	public class HelpCommand : ICommand
	{
		public CommandResult Execute(ExecutionSettings settings)
		{
			var output = settings.Output;

			string message = @"
Available commands:
  -- Set Credentials --
  set-credentials secret=<your_project_secret> - Sets credentials for all further requests made to MixPanel. Setting credentials is required to be done before any other export command execution.

  -- Raw Export --
  raw-export from=<starting date> to=<ending date> - Exports Raw data for specified date range
";

			output.Notify(message);												

			return new SuccessResult();
		}
	}
}