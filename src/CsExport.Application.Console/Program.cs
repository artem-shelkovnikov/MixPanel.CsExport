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
			commandParserConfigurationRegistry.InitializeFromAssebmlyOf<RawExportCommandConfiguration>();

			var application = new ExportConsoleApplication(new CommandParser(commandParserConfigurationRegistry),
				new MixPanelClient(new DefaultWebClient(), new MixPanelEndpointConfiguration(), new SigCalculator()),
				new ResultHandler(new ConsoleOutput()), new ConsoleInput(), new ClientConfiguration());

			while (true)
			{
				application.ReceiveCommand();
			}
		}
	}
}
