namespace CsExport.Application.Logic.Parser
{
	public class CommandParser : ICommandParser
	{
		private readonly ICommandParserConfigurationRegistry _commandParserConfigurationRegistry;

		public CommandParser(ICommandParserConfigurationRegistry commandParserConfigurationRegistry)
		{
			_commandParserConfigurationRegistry = commandParserConfigurationRegistry;
		}

		public ICommand ParseCommand(string commandText)
		{
			var parserConfigurations = _commandParserConfigurationRegistry.GetAll();
			foreach (var commandParserConfiguration in parserConfigurations)
			{
				var result = commandParserConfiguration.TryParse(commandText);
				if (result != null)
					return result;
			}

			return null;
		}
	}
}