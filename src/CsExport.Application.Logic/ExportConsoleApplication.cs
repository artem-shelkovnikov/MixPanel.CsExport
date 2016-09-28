using System;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Results;
using CsExport.Core.Client;

namespace CsExport.Application.Logic
{
	public class ExportConsoleApplication
	{
		private readonly ICommandParser _commandParser;
		private readonly IMixPanelClient _mixPanelClient;
		private readonly IResultHandler _resultHandler;

		private readonly IInputProvider _inputProvider;

		public ExportConsoleApplication(ICommandParser commandParser, IMixPanelClient mixPanelClient, IResultHandler resultHandler, IInputProvider inputProvider)
		{
			_commandParser = commandParser;
			_mixPanelClient = mixPanelClient;
			_resultHandler = resultHandler;
			_inputProvider = inputProvider;
		}

		public void ReceiveCommand()
		{
			try
			{
				var commandText = _inputProvider.GetInput();

				var command = _commandParser.ParseCommand(commandText);

				if (command == null)
				{
					_resultHandler.HandleResult(new CommandNotFoundResult(commandText));
					return;
				}

				var commandResult = command.Execute(_mixPanelClient, _inputProvider);
				_resultHandler.HandleResult(commandResult);
			}
			catch (Exception ex)
			{
				_resultHandler.HandleResult(new CommandTerminatedResult(ex));
			}
		}
	}
}