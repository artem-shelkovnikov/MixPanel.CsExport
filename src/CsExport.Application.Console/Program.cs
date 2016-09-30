using System.IO;
using CsExport.Application.Logic;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Configuration;
using CsExport.Core.Client;
using CsExport.Core.Settings;

namespace CsExport.Application.Console
{
	class Program
	{
		static void Main(string[] args)
		{															

			var commandParserConfigurationRegistry = new CommandParserConfigurationRegistry();				
			var commandParser = new CommandParser(commandParserConfigurationRegistry);

			var webClient = new DefaultWebClient();									  
			var mixPanelClient = new MixPanelClient(webClient);

			var fileWriter = new FileWriter();
			var input = new ConsoleInput();
			var output = new ConsoleOutput();
			var resultHandler = new ResultHandler(output);

			commandParserConfigurationRegistry.InitializeFromAssebmlyOf<RawExportCommandConfiguration>();

			var application = new ExportConsoleApplication(commandParser, mixPanelClient, resultHandler, fileWriter, input);

			while (true)
			{
				application.ReceiveCommand();
			}
		}
	}
}
