using System.Collections.Generic;

namespace CsExport.Application.Infrastructure.Parser
{
	public interface ICommandConfigurationRegistry
	{
		void AddMultiple(ICommandConfiguration[] configurations);

		IReadOnlyCollection<CommandDefinition> GetAll();
	}
}