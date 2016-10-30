using System;
using CsExport.Application.Logic.IO;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Results;
using CsExport.Core.Exceptions;

namespace CsExport.Application.Logic
{
	public class ExportConsoleApplication
	{
		private readonly ICommandParser _commandParser;
		private readonly IResultHandler _resultHandler;				   
		private readonly IInput _input;	   

		public ExportConsoleApplication(ICommandParser commandParser, IResultHandler resultHandler, IInput input)
		{
			_commandParser = commandParser;
			_resultHandler = resultHandler;
			_input = input;
		}

		public void ReceiveCommand()
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