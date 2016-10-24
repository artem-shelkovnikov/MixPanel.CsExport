using Autofac;
using CsExport.Application.Logic;
using CsExport.Application.Logic.CommandArguments;
using CsExport.Application.Logic.Commands;
using CsExport.Application.Logic.IO;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Configuration;
using CsExport.Core.Client;

namespace CsExport.Application.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var dependancyInjectionService = GetDependancyInjectionService();

			var commandParserConfigurationRegistry = new CommandParserConfigurationRegistry();
			var commandParser = new CommandParser(commandParserConfigurationRegistry,
			                                      new CommandFactory(dependancyInjectionService));


			var input = new ConsoleInput();
			var output = new ConsoleOutput();
			var resultHandler = new ResultHandler(output);

			commandParserConfigurationRegistry.InitializeFromAssebmlyOf<RawExportCommandConfiguration>();

			var application = new ExportConsoleApplication(commandParser, resultHandler, input);

			while (true)
			{
				application.ReceiveCommand();
			}
		}

		private static IDependancyInjectionService GetDependancyInjectionService()
		{
			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterType<MixPanelClient>().As<IMixPanelClient>();
			containerBuilder.RegisterType<FileWriter>().As<IFileWriter>();
			containerBuilder.RegisterType<DefaultWebClient>().As<IWebClient>();
			containerBuilder.RegisterType<ConsoleInput>().As<IInput>();
			containerBuilder.RegisterType<ConsoleOutput>().As<IOutput>();
			containerBuilder.RegisterType<HelpCommand>().As<ICommandWithArguments<HelpCommandArguments>>();
			containerBuilder.RegisterType<RawExportCommand>().As<ICommandWithArguments<RawExportCommandArguments>>();
			containerBuilder.RegisterType<SetCredentialsCommand>().As<ICommandWithArguments<SetCredentialsCommandArguments>>();

			var dependancyInjectionService = new DependancyInjectionService(containerBuilder.Build());

			return dependancyInjectionService;
		}
	}
}