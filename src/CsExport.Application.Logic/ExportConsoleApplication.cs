using System;
using System.IO;
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
		private readonly IFileWriter _fileWriter;
		private readonly ClientConfiguration _clientConfiguration;
		private readonly ApplicationConfiguration _applicationConfiguration;

		private readonly IInput _input;
		private readonly IOutput _output;

		public ExportConsoleApplication(ICommandParser commandParser, IMixPanelClient mixPanelClient, IResultHandler resultHandler, IFileWriter fileWriter, IInput input, IOutput output)
		{
			_commandParser = commandParser;
			_mixPanelClient = mixPanelClient;
			_resultHandler = resultHandler;
			_fileWriter = fileWriter;
			_input = input;
			_output = output;
			_clientConfiguration = new ClientConfiguration();
			_applicationConfiguration = new ApplicationConfiguration
			{
				ExportPath = Directory.GetCurrentDirectory()
			};
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
					Output = _output,
					MixPanelClient = _mixPanelClient,
					FileWriter = _fileWriter,
					ClientConfiguration = _clientConfiguration,
					ApplicationConfiguration = _applicationConfiguration
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