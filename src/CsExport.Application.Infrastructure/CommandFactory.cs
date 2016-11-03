using System;
using CsExport.Application.Infrastructure.DependancyControl;

namespace CsExport.Application.Infrastructure
{
	internal class CommandFactory : ICommandFactory
	{
		private readonly IDependancyContainer _dependancyContainer;

		public CommandFactory(IDependancyContainer dependancyContainer)
		{
			_dependancyContainer = dependancyContainer;
		}

		public ICommand Create(IArguments arguments)
		{
			var commandType = typeof(ICommandWithArguments<>);
			var genericType = commandType.MakeGenericType(arguments.GetType());

			var command = _dependancyContainer.Resolve(genericType);

			if (command == null)
				return null;

			var compiledCommandType = typeof(CompiledCommand<,>);
			var genericCompiledCommandType = compiledCommandType.MakeGenericType(genericType, arguments.GetType());

			var result = (ICommand) Activator.CreateInstance(genericCompiledCommandType, command, arguments);

			return result;
		}
	}
}