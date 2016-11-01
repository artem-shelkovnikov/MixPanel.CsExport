using CsExport.Application.Logic.Parser;
using CsExport.Application.Logic.Parser.Configuration;

namespace CsExport.Application.Logic.DependancyControl
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