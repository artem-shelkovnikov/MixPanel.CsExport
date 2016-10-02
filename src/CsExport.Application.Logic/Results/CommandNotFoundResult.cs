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
			var message = string.Format("Command not recognized: {0} \nType \"help\" to get a list of available commands", _commandText);

			output.Notify(message);
		}
	}
}