using System;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.IO;
using CsExport.Application.Logic.Results;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Commands
{
	public class HelpCommand : ICommandWithArguments<HelpCommandArguments>
	{
		private readonly IOutput _output;

		public HelpCommand(IOutput output)
		{
			_output = output;
		}

		public CommandResult Execute(ApplicationConfiguration applicationConfiguration,
		                             ClientConfiguration clientConfiguration,
		                             HelpCommandArguments arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			string message = @"
Available commands:
  -- Set Credentials --
  set-credentials secret=<your_project_secret> - Sets credentials for all further requests made to MixPanel. Setting credentials is required to be done before any other export command execution.

  -- Raw Export --
  raw-export from=<starting date> to=<ending date> - Exports Raw data for specified date range
";

			_output.Notify(message);

			return new SuccessResult();
		}
	}
}