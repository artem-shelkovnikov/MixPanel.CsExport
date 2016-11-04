using CsExport.Application.Console.Infrastructure;
using CsExport.Application.Infrastructure;
using CsExport.Application.Logic.Binders;
using CsExport.Core;

namespace CsExport.Application.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var bootstrapper = new ConsoleApplicationBootstrapper();

			bootstrapper.RegisterCommands(new ApplicationCommandRegistration())
			            .ConfigureDependancies(new ApplicationDependancyRegistration());
			
			bootstrapper.ValueBinders.Add<Date>((o, info) => new DateValueBinder(o, info));

			bootstrapper.Run();
		}
	}
}