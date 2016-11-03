using CsExport.Application.Infrastructure.Builtin.Configuration;
using CsExport.Application.Infrastructure.FluentConfiguration;
using CsExport.Application.Infrastructure.Parser;

namespace CsExport.Application.Infrastructure.DependancyControl
{
	internal class DefaultCommandRegistration : CommandRegistration
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