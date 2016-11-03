using CsExport.Application.Infrastructure.DependancyControl;
using CsExport.Application.Infrastructure.FluentConfiguration;
using CsExport.Application.Infrastructure.IO;
using CsExport.Application.Infrastructure.Parser;

namespace CsExport.Application.Infrastructure
{
	internal class ConsoleApplicationFactory : IConsoleApplicationFactory
	{
		public IConsoleApplication Create(ICommandConfigurationRegistry commandConfigurationRegistry,
		                                  IDependancyContainer dependancyContainer)
		{
			var commandParser = new CommandParser(commandConfigurationRegistry,
			                                      new CommandFactory(dependancyContainer),
			                                      new CommandArgumentParser());
			var resultHandler = new ResultHandler(new ConsoleOutput());
			var consoleInput = new ConsoleInput();

			return new ConsoleApplication(commandParser, resultHandler, consoleInput);
		}
	}
}