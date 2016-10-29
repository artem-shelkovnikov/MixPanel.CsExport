using System.Linq;
using CsExport.Application.Logic.Parser;
using Xunit;

namespace CsExport.Application.Logic.Tests.ParserTests
{
	public class CommandConfigurationRegistryTests
	{
		private const string CommandSignature = "stub-command";
		private const string CommandDescription = "stub description";
		private const string ParameterName = "parameter-name";
		private const string ParameterDescription = "parameter-description";

		private readonly ICommandConfigurationRegistry _configurationRegistry = new CommandConfigurationRegistry();

		[Fact]
		public void Init_When_called_Then_collects_profiles_from_calling_assembly()
		{
			_configurationRegistry.InitializeFromAssebmlyOf<StubParserConfiguration>();

			var existingConfigurations = _configurationRegistry.GetAll();

			Assert.Equal(1, existingConfigurations.Count());
		}

		[Fact]
		public void Init_When_called_Then_unmapped_parameters_are_not_used()
		{
			_configurationRegistry.InitializeFromAssebmlyOf<StubParserConfiguration>();

			var existingConfigurations = _configurationRegistry.GetAll();
			var stubConfiguration = existingConfigurations.Single();

			Assert.Equal(1, stubConfiguration.Parameters.Count());
		}

		[Fact]
		public void Init_When_called_Then_configuration_contains_specified_command_definition()
		{
			_configurationRegistry.InitializeFromAssebmlyOf<StubParserConfiguration>();

			var existingConfigurations = _configurationRegistry.GetAll();
			var stubConfiguration = existingConfigurations.Single();
			var configurationParameter = stubConfiguration.Parameters.Single();

			Assert.Equal(typeof(StubCommandArguments), stubConfiguration.Type);
			Assert.Equal(CommandSignature, stubConfiguration.Signature);
			Assert.Equal(CommandDescription, stubConfiguration.Description);
			Assert.Equal(ParameterName, configurationParameter.Signature);
			Assert.Equal(ParameterDescription, configurationParameter.Description);
		}

		public class StubCommandArguments : IArguments
		{
			public string Name { get; set; }

			public string Description { get; set; }
		}

		public class StubParserConfiguration : CommandConfiguration<StubCommandArguments>
		{
			public StubParserConfiguration()
			{
				HasSignature(CommandSignature);

				HasDescription(CommandDescription);

				HasArgument(x => x.Name)
					.WithSignature(ParameterName)
					.WithDescription(ParameterDescription);
			}
		}
	}
}