using CsExport.Application.Logic.DependancyControl;
using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Configuration;

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