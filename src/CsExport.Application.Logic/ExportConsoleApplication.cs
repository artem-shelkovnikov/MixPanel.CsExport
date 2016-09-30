using System;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Results;
using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Logic
{
	public class ExportConsoleApplication
	{
		private readonly ICommandParser _commandParser;
		private readonly IMixPanelClient _mixPanelClient;
		private readonly IResultHandler _resultHandler;
		private readonly ClientConfiguration _clientConfiguration;

		private readonly IInput _input;	

		public ExportConsoleApplication(ICommandParser commandParser, IMixPanelClient mixPanelClient, IResultHandler resultHandler, IInput input, ClientConfiguration clientConfiguration)
		{
			_commandParser = commandParser;
			_mixPanelClient = mixPanelClient;
			_resultHandler = resultHandler;
			_input = input;
			_clientConfiguration = clientConfiguration;
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

				var executionSettings = new ExecutionSettings
				{
					Input = _input,
					MixPanelClient = _mixPanelClient,
					ClientConfiguration = _clientConfiguration
				};

				var commandResult = command.Execute(executionSettings);
				_resultHandler.HandleResult(commandResult);
			}
			catch (Exception ex)
			{
				_resultHandler.HandleResult(new CommandTerminatedResult(ex));
			}
		}
	}
}