using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsExport.Application.Infrastructure.Parser
{
	public class CommandConfigurationRegistry : ICommandConfigurationRegistry
	{
		private readonly List<CommandDefinition> _configurations = new List<CommandDefinition>();

		public void AddMultiple(ICommandConfiguration[] configurations)
		{
			AddDefinitions(configurations);
		}

		private void AddDefinitions(ICommandConfiguration[] configurationImplementations)
		{
			var commandDefinitions = configurationImplementations.Select(x =>
			                                                             {
				                                                             var property = x.GetType()
				                                                                             .GetProperty("Configuration",
				                                                                                          BindingFlags.IgnoreCase
				                                                                                          | BindingFlags.NonPublic
				                                                                                          | BindingFlags.Instance);
				                                                             var value =
					                                                             (CommandDefinition) property.GetValue(x, null);
				                                                             return value;
			                                                             });

			if (
				commandDefinitions.Any(
					x =>
						commandDefinitions.Count(y => y.Signature == x.Signature) > 1
						|| _configurations.Any(y => y.Signature == x.Signature)))
				throw new ArgumentException(
					"Multiple commands with same signature detected. Each command signature must be unique.",
					nameof(configurationImplementations));


			_configurations.AddRange(
				commandDefinitions);
		}

		public IReadOnlyCollection<CommandDefinition> GetAll()
		{
			return _configurations;
		}
	}
}