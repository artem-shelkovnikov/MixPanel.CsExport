using System;
using System.Collections.Generic;
using System.Linq;
using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public abstract class CommandParserConfigurationBase : ICommandParserConfiguration
	{
		private ConsoleCommandParser _consoleCommandParser = new ConsoleCommandParser();
		public ICommand TryParse(string input)
		{
			var commandDefinition = _consoleCommandParser.Parse(input);
			if (commandDefinition == null)
				return null;

			return ParseInner(commandDefinition.Name, commandDefinition.Arguments);
		}

		protected abstract ICommand ParseInner(string commandName, IEnumerable<CommandArgument> arguments);	 
	}
}