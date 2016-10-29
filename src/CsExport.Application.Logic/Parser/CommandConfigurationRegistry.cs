using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsExport.Application.Logic.Parser
{
	public class CommandConfigurationRegistry : ICommandConfigurationRegistry
	{
		private static List<CommandDefinition> _configurations = new List<CommandDefinition>();

		public void InitializeFromAssebmlyOf<T>() where T : ICommandConfiguration
		{
			var configurationImplementations = typeof(T).Assembly
			                                            .GetTypes()
			                                            .Where(
				                                            x =>
					                                            typeof(ICommandConfiguration).IsAssignableFrom(x)
					                                            && x.IsAbstract == false).ToArray();

			_configurations =
				configurationImplementations.Select(x =>
				                                    {
					                                    var property = x.GetProperty("Configuration",
					                                                                 BindingFlags.IgnoreCase | BindingFlags.NonPublic
					                                                                 | BindingFlags.Instance);
					                                    var value =
						                                    (CommandDefinition) property.GetValue(Activator.CreateInstance(x), null);
					                                    return value;
				                                    })
				                            .ToList();
		}

		public IReadOnlyCollection<CommandDefinition> GetAll()
		{
			return _configurations;
		}
	}
}