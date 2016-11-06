using CsExport.Application.Console.Infrastructure;
using CsExport.Application.Infrastructure;
using CsExport.Application.Logic.Binders;
using CsExport.Application.Logic.Results;
using CsExport.Core;
using CsExport.Core.Exceptions;

namespace CsExport.Application.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var bootstrapper = new ConsoleApplicationBootstrapper();

			bootstrapper.RegisterCommands(new ApplicationCommandRegistration())
			            .ConfigureDependancies(new ApplicationDependancyRegistration());

			bootstrapper.ValueBinders.AddOrUpdate<Date>((o, info) => new DateValueBinder(o, info));
			bootstrapper.ExceptionHandlers.AddOrUpdate<MixPanelUnauthorizedException>(exception => new UnauthorizedResult());

			bootstrapper.Run();
		}
	}
}