using CsExport.Application.Logic.IO;

namespace CsExport.Application.Logic.Results
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