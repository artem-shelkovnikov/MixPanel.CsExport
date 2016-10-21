using System.Collections.Generic;

namespace CsExport.Application.Logic.Parser
{
	public interface ICommandParserConfigurationRegistry
	{
		void InitializeFromAssebmlyOf<T>() where T : ICommandParserConfiguration;

		IEnumerable<ICommandParserConfiguration> GetAll();

		ICommandParserConfiguration GetByName(string commandName);
	}
}