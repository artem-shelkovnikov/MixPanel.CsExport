using System.Collections.Generic;

namespace CsExport.Application.Infrastructure.FluentConfiguration
{
	public interface ICommandConfigurationRegistry
	{
		void AddMultiple(ICommandConfiguration[] configurations);

		IReadOnlyCollection<CommandDefinition> GetAll();
	}
}