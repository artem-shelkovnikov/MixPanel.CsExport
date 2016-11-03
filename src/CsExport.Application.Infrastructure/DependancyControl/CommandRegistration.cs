using CsExport.Application.Infrastructure.FluentConfiguration;
using CsExport.Application.Infrastructure.Parser;

namespace CsExport.Application.Infrastructure.DependancyControl
{
	public abstract class CommandRegistration
	{
		public void Load(ICommandConfigurationRegistry commandConfigurationRegistry)
		{
			var configurations = Load();
			commandConfigurationRegistry.AddMultiple(configurations);
		}

		protected abstract ICommandConfiguration[] Load();
	}
}