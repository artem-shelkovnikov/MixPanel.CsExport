using System.Collections.Generic;
using System.Reflection;
using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Parser
{
	public abstract class CommandParserConfigurationBase<TCommandArguments> : ICommandParserConfiguration
		where TCommandArguments : IArguments, new()
	{
		private readonly IReflectionPropertyBinderFactory _binderFactory = new ReflectionPropertyBinderFactory();

		public abstract string CommandName { get; }

		public IArguments TryParse(IEnumerable<CommandArgument> arguments)
		{
			var commandArgumentsInstance = new TCommandArguments();

			var typeDefinition = typeof(TCommandArguments);

			foreach (var commandArgument in arguments)
			{
				var propertyDefinition = typeDefinition.GetProperty(commandArgument.ArgumentName,
				                                                    BindingFlags.IgnoreCase | BindingFlags.Public
				                                                    | BindingFlags.Instance);
				var propertyValueBinder = _binderFactory.CreateForProperty(commandArgumentsInstance, propertyDefinition);

				if (propertyValueBinder != null)
					propertyValueBinder.BindValue(commandArgument.Value);
			}

			return commandArgumentsInstance;
		}
	}
}