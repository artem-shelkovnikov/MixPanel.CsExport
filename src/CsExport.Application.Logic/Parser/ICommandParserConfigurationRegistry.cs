using System.Collections.Generic;

namespace CsExport.Application.Logic.Parser
{
	public interface ICommandParserConfigurationRegistry
	{
		void Initialize();
		IEnumerable<ICommandParserConfiguration> GetAll();
	}
}