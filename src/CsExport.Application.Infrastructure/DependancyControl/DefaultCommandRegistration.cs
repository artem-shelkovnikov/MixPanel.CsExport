using CsExport.Application.Infrastructure.Builtin.Configuration;
using CsExport.Application.Infrastructure.FluentConfiguration;

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