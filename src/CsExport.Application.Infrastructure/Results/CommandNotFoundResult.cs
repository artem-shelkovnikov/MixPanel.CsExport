using CsExport.Application.Infrastructure.IO;

namespace CsExport.Application.Infrastructure.Results
{
	public class CommandNotFoundResult : CommandResult
	{
		private readonly string _commandText;

		public CommandNotFoundResult(string commandText)
		{
			_commandText = commandText;
		}

		public override void Handle(IOutput output)
		{
			var message = $"Command not recognized: {_commandText} \nType \"help\" to get a list of available commands";

			output.Notify(message);
		}
	}
}