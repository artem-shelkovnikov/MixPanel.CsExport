using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsExport.Application.Logic.Parser
{
	public class CommandParserConfigurationRegistry : ICommandParserConfigurationRegistry
	{		
		private ICollection<ICommandParserConfiguration> _configurations = new List<ICommandParserConfiguration>();

		public void Initialize()
		{
			_configurations = Assembly.GetCallingAssembly()
				.GetTypes()
				.Where(x => typeof(ICommandParserConfiguration).IsAssignableFrom(x))
				.Select(x => (ICommandParserConfiguration) Activator.CreateInstance(x))
				.ToList();
		}

		public IEnumerable<ICommandParserConfiguration> GetAll()
		{
			return _configurations;
		}
	}
}