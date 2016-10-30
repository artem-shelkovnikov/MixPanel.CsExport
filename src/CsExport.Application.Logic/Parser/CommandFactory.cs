﻿using System;
using CsExport.Application.Logic.Parser.Utility;

namespace CsExport.Application.Logic.Parser
{
	public class CommandFactory : ICommandFactory
	{
		private readonly IDependancyInjectionService _dependancyInjectionService;

		public CommandFactory(IDependancyInjectionService dependancyInjectionService)
		{
			_dependancyInjectionService = dependancyInjectionService;
		}

		public ICommand Create(IArguments arguments)
		{
			var commandType = typeof(ICommandWithArguments<>);
			var genericType = commandType.MakeGenericType(arguments.GetType());

			var command = _dependancyInjectionService.Resolve(genericType);

			if (command == null)
				return null;

			var compiledCommandType = typeof(CompiledCommand<,>);
			var genericCompiledCommandType = compiledCommandType.MakeGenericType(genericType, arguments.GetType());

			var result = (ICommand) Activator.CreateInstance(genericCompiledCommandType, command, arguments);

			return result;
		}
	}
}