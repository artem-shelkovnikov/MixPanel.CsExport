using System;
using System.Collections.Generic;
using System.Linq;

namespace CsExport.Application.Logic.Parser.Utility
{
	public class ConsoleCommandParser
	{
		private const char WordDelimiter = ' ';
		private const char ArgumentDelimiter = '-';
		private const char ArgumentValueDelimiter = '=';

		public ConsoleCommandDefinition Parse(string input)
		{
			string commandName;
			IEnumerable<CommandArgument> arguments = null;

			var nameDelimiterIndex = input.IndexOf(WordDelimiter.ToString(), StringComparison.InvariantCulture);
			if (nameDelimiterIndex == -1)
			{
				commandName = input;
			}
			else
			{
				commandName = input.Substring(0, nameDelimiterIndex);
				var argumentsPart = input.Substring(nameDelimiterIndex).Trim();
				arguments = ParseArguments(argumentsPart);
			}

			return new ConsoleCommandDefinition
			{
				Name = commandName,
				Arguments = arguments ?? Enumerable.Empty<CommandArgument>()
			};
		}

		private static IEnumerable<CommandArgument> ParseArguments(string argumentsPart)
		{
			var result = new List<CommandArgument>();

			string buffer = string.Empty;

			for (var i = 0; i < argumentsPart.Length; i++)
			{
				var currentSymbol = argumentsPart.ElementAt(i);
				var nextSymbol = i == argumentsPart.Length - 1
					? (char?)null
					: argumentsPart.ElementAt(i + 1);

				buffer += currentSymbol;

				var argumentDefinitionEnded = nextSymbol == null
				                              || (nextSymbol == ArgumentDelimiter
				                                  && (currentSymbol == WordDelimiter));

				if (argumentDefinitionEnded)
				{
					var argumentDefinition = ParseArgument(buffer.Trim());
					buffer = String.Empty;
					result.Add(argumentDefinition);
				}
			}

			if (string.IsNullOrWhiteSpace(buffer) == false)
				throw new ArgumentParseException(buffer);

			return result;
		}

		private static CommandArgument ParseArgument(string argumentDefinition)
		{
			string argumentName = null;
			string argumentValue = null;

			if (argumentDefinition.StartsWith(ArgumentDelimiter.ToString()) == false)
				throw new ArgumentParseException(argumentDefinition);

			var argumentValueDelimiterIndex = argumentDefinition.IndexOf(ArgumentValueDelimiter);
			if (argumentValueDelimiterIndex != -1)
			{
				argumentName = argumentDefinition
				.Substring(0, argumentValueDelimiterIndex)
					.Substring(1);
				argumentValue = argumentDefinition
				.Substring(argumentValueDelimiterIndex)
					.Substring(1);
			}
			else
			{
				argumentName = argumentDefinition
					.Substring(1);
			}

			return new CommandArgument
			{
				ArgumentName = argumentName,
				Value = argumentValue
			};
		}
	}

	public class ConsoleCommandDefinition
	{
		public string Name { get; set; }
		public IEnumerable<CommandArgument> Arguments { get; set; }
	}
}