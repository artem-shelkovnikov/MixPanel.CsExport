using System;
using System.Text;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.IO;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Results;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic.Commands
{
	public class HelpCommand : ICommandWithArguments<HelpCommandArguments>
	{
		private readonly IOutput _output;
		private readonly ICommandConfigurationRegistry _commandConfigurationRegistry;

		public HelpCommand(IOutput output, ICommandConfigurationRegistry commandConfigurationRegistry)
		{
			_output = output;
			_commandConfigurationRegistry = commandConfigurationRegistry;
		}

		public CommandResult Execute(HelpCommandArguments arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			var commands = _commandConfigurationRegistry.GetAll();

			var builder = new StringBuilder();

			foreach (var commandParserConfiguration in commands)
			{
				builder.AppendLine(commandParserConfiguration.Signature);
				builder.AppendLine(commandParserConfiguration.Description);

				foreach (var propertyConfiguration in commandParserConfiguration.Parameters)
				{
					builder.AppendLine("-" + propertyConfiguration.Signature + " -- " + propertyConfiguration.Description);
				}

				builder.AppendLine();
			}

			_output.Notify(builder.ToString());

			return new SuccessResult();
		}
	}
}