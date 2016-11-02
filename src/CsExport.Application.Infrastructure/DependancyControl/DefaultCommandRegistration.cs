using CsExport.Application.Infrastructure.Configuration;
using CsExport.Application.Infrastructure.Parser;

namespace CsExport.Application.Infrastructure.DependancyControl
{
	public class DefaultCommandRegistration : CommandRegistration
	{
		protected override ICommandConfiguration[] Load()
		{
			return new[]
			{
				new HelpCommandConfiguration(),
			};
		}
	}
}