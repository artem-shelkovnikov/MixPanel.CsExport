using CsExport.Application.Logic.Parser;

namespace CsExport.Application.Logic.DependancyControl
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