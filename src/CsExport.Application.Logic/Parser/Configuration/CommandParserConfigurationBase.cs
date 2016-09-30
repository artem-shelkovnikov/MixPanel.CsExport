using System.Collections.Generic;
using System.Linq;

namespace CsExport.Application.Logic.Parser.Configuration
{
	public abstract class CommandParserConfigurationBase : ICommandParserConfiguration
	{
		public ICommand TryParse(string input)
		{
			var inputSections = input.Split(' ');
			var commandName = inputSections[0];

			if (commandName == null || string.IsNullOrWhiteSpace(commandName))
				return null;

			var arguments = inputSections
				.Skip(1)
				.Where(x => x.StartsWith("-"))
				.Select(x => x.Remove(0, 1))
				.Select(x => x.Split('='))
				.ToDictionary(x => x[0], x => x[1]);

			var commandStructure = new CommandStructure
			{
				CommandName = commandName,
				Arguments = arguments.Select(x => new CommandStructure.CommandArgument
				{
					ArgumentName = x.Key,
					Value = x.Value
				})
			};

			return ParseInner(commandStructure);
		}

		protected abstract ICommand ParseInner(CommandStructure commandStructure);

		protected class CommandStructure
		{
			public string CommandName { get; set; }
			public IEnumerable<CommandArgument> Arguments { get; set; }

			public class CommandArgument
			{
				public string ArgumentName { get; set; }
				public string Value { get; set; }
			}
		}
	}
}