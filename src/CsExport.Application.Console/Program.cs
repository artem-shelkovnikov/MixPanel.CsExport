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
		private const string DefaultExportDirectory = "export";

		static void Main(string[] args)
		{
			if (Directory.Exists(DefaultExportDirectory) == false)
				Directory.CreateDirectory(DefaultExportDirectory);

			var commandParserConfigurationRegistry = new CommandParserConfigurationRegistry();
			commandParserConfigurationRegistry.InitializeFromAssebmlyOf<RawExportCommandConfiguration>();

			var commandParser = new CommandParser(commandParserConfigurationRegistry);

			var webClient = new DefaultWebClient();

			var mixPanelEndpointConfiguration = new MixPanelEndpointConfiguration();

			var mixPanelClient = new MixPanelClient(webClient, mixPanelEndpointConfiguration);

			var fileWriter = new FileWriter();

			var input = new ConsoleInput();
			var output = new ConsoleOutput();

			var resultHandler = new ResultHandler(output);

			var defaultClientConfiguration = new ClientConfiguration();

			var defaultApplicationConfiguration = new ApplicationConfiguration
			{
				ExportPath = Path.GetFullPath(".") + "\\" + DefaultExportDirectory
			};

			var application = new ExportConsoleApplication(commandParser, mixPanelClient, resultHandler, fileWriter, input, defaultClientConfiguration, defaultApplicationConfiguration);

			while (true)
			{
				application.ReceiveCommand();
			}
		}
	}
}
