using CsExport.Application.Logic.DependancyControl;
using CsExport.Application.Logic.IO;
using CsExport.Application.Logic.Parser;

namespace CsExport.Application.Logic
{
	class ConsoleApplicationFactory : IConsoleApplicationFactory
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