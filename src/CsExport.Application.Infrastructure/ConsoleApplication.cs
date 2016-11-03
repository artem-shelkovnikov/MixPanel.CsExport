﻿using System;
using CsExport.Application.Infrastructure.IO;
using CsExport.Application.Infrastructure.Parser;
using CsExport.Application.Infrastructure.Results;
using CsExport.Core.Exceptions;

namespace CsExport.Application.Infrastructure
{
	internal class ConsoleApplication : IConsoleApplication
	{
		private readonly ICommandParser _commandParser;
		private readonly IResultHandler _resultHandler;
		private readonly IInput _input;

		public ConsoleApplication(ICommandParser commandParser, IResultHandler resultHandler, IInput input)
		{
			_commandParser = commandParser;
			_resultHandler = resultHandler;
			_input = input;
		}

		public bool IsTerminated()
		{
			return false;
		}

		public void ReadCommand()
		{
			try
			{
				var commandText = _input.GetLine();

				var command = _commandParser.ParseCommand(commandText);

				if (command == null)
				{
					_resultHandler.HandleResult(new CommandNotFoundResult(commandText));
					return;
				}

				var commandResult = command.Execute();

				_resultHandler.HandleResult(commandResult);
			}
			catch (MixPanelUnauthorizedException)
			{
				_resultHandler.HandleResult(new UnauthorizedResult());
			}
			catch (Exception ex)
			{
				_resultHandler.HandleResult(new CommandTerminatedResult(ex));
			}
		}
	}
}