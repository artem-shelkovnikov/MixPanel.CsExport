using System;
using System.Linq;
using CsExport.Application.Logic.Parser;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests
{
	public class CommandParserConfigurationRegistryTests
	{
		private ICommandParserConfigurationRegistry _configurationRegistry = new CommandParserConfigurationRegistry();

		[Fact]
		public void Init_When_called_Then_collects_profiles_from_calling_assembly()
		{
			_configurationRegistry.Initialize();

			var existingConfigurations = _configurationRegistry.GetAll();
			var configurationTypes = existingConfigurations.Select(x => x.GetType()).ToArray();

			Assert.NotEqual(0, existingConfigurations.Count());
			Assert.Contains(typeof(StubParserConfiguration), configurationTypes); 
		}

		public class StubParserConfiguration : ICommandParserConfiguration
		{
			public ICommand TryParse(string input)
			{
				throw new NotImplementedException();
			}
		}
	}
}