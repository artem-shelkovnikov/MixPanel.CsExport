using CsExport.Application.Infrastructure.DependancyControl;
using CsExport.Application.Infrastructure.FluentConfiguration;
using CsExport.Application.Logic.Configuration;

namespace CsExport.Application.Console.Infrastructure
{
	public class ApplicationCommandRegistration : CommandRegistration
	{
		protected override ICommandConfiguration[] Load()
		{
			return new ICommandConfiguration[]
			{
				new SetCredentialsCommandConfiguration(),
				new RawExportCommandConfiguration()
			};
		}
	}
}