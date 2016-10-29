using System.Collections.Generic;

namespace CsExport.Application.Logic.Parser
{
	public interface ICommandConfigurationRegistry
	{
		void InitializeFromAssebmlyOf<T>() where T : ICommandConfiguration;

		IReadOnlyCollection<CommandDefinition> GetAll();
	}
}