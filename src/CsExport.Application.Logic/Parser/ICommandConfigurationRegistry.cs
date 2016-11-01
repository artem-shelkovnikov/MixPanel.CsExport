using System.Collections.Generic;

namespace CsExport.Application.Logic.Parser
{
	public interface ICommandConfigurationRegistry
	{
		void AddMultiple(ICommandConfiguration[] configurations);

		IReadOnlyCollection<CommandDefinition> GetAll();
	}
}