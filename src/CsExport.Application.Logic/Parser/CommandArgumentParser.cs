using System;
using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Parser
{
	public class CommandArgumentParser : ICommandArgumentParser
	{
		private readonly IReflectionPropertyBinderFactory _binderFactory;
		private readonly IParameterExtractor _parameterExtractor;

		public CommandArgumentParser() : this(new ReflectionPropertyBinderFactory(), new DefaultParameterExtractor())
		{
		}

		internal CommandArgumentParser(IReflectionPropertyBinderFactory binderFactory, IParameterExtractor parameterExtractor)
		{
			_binderFactory = binderFactory;
			_parameterExtractor = parameterExtractor;
		}

		public bool CanParse(string commandText, CommandDefinition commandDefinition)
		{
			if (string.IsNullOrWhiteSpace(commandText))
				return false;

			if (commandDefinition == null)
				return false;

			return commandText.StartsWith($"{commandDefinition.Signature} ", StringComparison.InvariantCultureIgnoreCase)
			       || commandText.Equals(commandDefinition.Signature);
		}

		public IArguments Parse(string commandText, CommandDefinition commandDefinition)
		{
			var typeDefinition = commandDefinition.Type;

			if (CanParse(commandText, commandDefinition) == false)
				return null;

			var argumentsInput = StripCommandSignature(commandText, commandDefinition.Signature);

			IArguments commandArgumentsInstance = (IArguments) Activator.CreateInstance(typeDefinition);

			foreach (var commandParameter in commandDefinition.Parameters)
			{
				var propertyDefinition = commandParameter.PropertyInfo;

				var propertyValueBinder = _binderFactory.CreateForProperty(commandArgumentsInstance, propertyDefinition);

				var propertyValue = _parameterExtractor.ExtractValue(argumentsInput, commandParameter.Signature);

				if (propertyValueBinder != null)
					propertyValueBinder.BindValue(propertyValue);
			}

			return commandArgumentsInstance;
		}

		private string StripCommandSignature(string commandText, string commandSignature)
		{
			return commandText
				.Remove(0, commandSignature.Length)
				.Trim();
		}
	}
}