using CsExport.Application.Console.Infrastructure;
using CsExport.Application.Logic;

namespace CsExport.Application.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var bootstrapper = new ConsoleApplicationBootstrapper();
			bootstrapper.RegisterCommands(new ApplicationCommandRegistration())
			            .ConfigureDependancies(new ApplicationDependancyRegistration());


			bootstrapper.Run();
		}
	}
}