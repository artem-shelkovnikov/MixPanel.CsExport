using System.Linq;

namespace CsExport.Application.Infrastructure.Parser
{
	public class CommandParser : ICommandParser
	{
		private readonly ICommandConfigurationRegistry _commandConfigurationRegistry;
		private readonly ICommandFactory _commandFactory;
		private readonly ICommandArgumentParser _commandArgumentParser;

		public CommandParser(ICommandConfigurationRegistry commandConfigurationRegistry,
		                     ICommandFactory commandFactory,
		                     ICommandArgumentParser commandArgumentParser)
		{
			_commandConfigurationRegistry = commandConfigurationRegistry;
			_commandFactory = commandFactory;
			_commandArgumentParser = commandArgumentParser;
		}

		public ICommand ParseCommand(string commandText)
		{
			var commandDefinitions = _commandConfigurationRegistry.GetAll();

			if (commandDefinitions == null
			    || commandDefinitions.Any() == false)
				return null;

			var commandDefinition = commandDefinitions.FirstOrDefault(x => _commandArgumentParser.CanParse(commandText, x));

			if (commandDefinition == null)
				return null;

			var arguments = _commandArgumentParser.Parse(commandText, commandDefinition);

			var command = _commandFactory.Create(arguments);

			return command;
		}
	}
}