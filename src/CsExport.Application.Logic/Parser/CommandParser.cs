using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Parser
{
	public class CommandParser : ICommandParser
	{
		private readonly ICommandParserConfigurationRegistry _commandParserConfigurationRegistry;
		private readonly ICommandFactory _commandFactory;
		private readonly ConsoleCommandParser _consoleCommandParser = new ConsoleCommandParser();

		public CommandParser(ICommandParserConfigurationRegistry commandParserConfigurationRegistry,
		                     ICommandFactory commandFactory)
		{
			_commandParserConfigurationRegistry = commandParserConfigurationRegistry;
			_commandFactory = commandFactory;
		}

		public ICommand ParseCommand(string commandText)
		{
			var commandDefinition = _consoleCommandParser.Parse(commandText);

			var commandParserConfiguration = _commandParserConfigurationRegistry.GetByName(commandDefinition.Name);

			if (commandParserConfiguration == null)
				return null;

			var arguments = commandParserConfiguration.TryParse(commandDefinition.Arguments);

			var command = _commandFactory.Create(arguments);

			return command;
		}
	}
}