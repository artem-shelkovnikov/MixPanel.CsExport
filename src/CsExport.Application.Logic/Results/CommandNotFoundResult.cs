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
			output.Notify(string.Format("Command not recognized: {0}", _commandText));
		}
	}
}