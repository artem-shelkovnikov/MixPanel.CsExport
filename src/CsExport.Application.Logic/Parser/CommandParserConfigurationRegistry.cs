using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsExport.Application.Logic.Parser
{
	public class CommandParserConfigurationRegistry : ICommandParserConfigurationRegistry
	{		
		private ICollection<ICommandParserConfiguration> _configurations = new List<ICommandParserConfiguration>();

		public void InitializeFromAssebmlyOf<T>() where T : ICommandParserConfiguration
		{
			_configurations = typeof(T).Assembly
				.GetTypes()
				.Where(x => typeof(ICommandParserConfiguration).IsAssignableFrom(x) && x.IsAbstract == false)
				.Select(x => (ICommandParserConfiguration) Activator.CreateInstance(x))
				.ToList();
		}

		public IEnumerable<ICommandParserConfiguration> GetAll()
		{
			return _configurations;
		}
	}
}