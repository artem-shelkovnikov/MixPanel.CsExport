using System;
using CsExport.Application.Infrastructure.IO;
using CsExport.Application.Infrastructure.Parser;
using CsExport.Application.Infrastructure.Results;

namespace CsExport.Application.Infrastructure
{
	internal class ConsoleApplication : IConsoleApplication
	{
		private readonly ICommandParser _commandParser;
		private readonly IResultHandler _resultHandler;
		private readonly IExceptionHandler _exceptionHandler;
		private readonly IInput _input;

		public ConsoleApplication(ICommandParser commandParser,
		                          IResultHandler resultHandler,
		                          IInput input,
		                          IExceptionHandler exceptionHandler)
		{
			_commandParser = commandParser;
			_resultHandler = resultHandler;
			_input = input;
			_exceptionHandler = exceptionHandler;
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
			catch (Exception ex)
			{
				var result = _exceptionHandler.HandleException(ex);
				_resultHandler.HandleResult(result);
			}
		}
	}
}