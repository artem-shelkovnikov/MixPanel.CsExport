using System;
using System.Linq;
using CsExport.Application.Infrastructure.FluentConfiguration;
using Xunit;

namespace CsExport.Application.Infrastructure.Tests.ParserTests
{
	public class CommandConfigurationRegistryTests
	{
		private const string CommandSignature = "stub-command";
		private const string CommandDescription = "stub description";
		private const string ParameterName = "parameter-name";
		private const string ParameterDescription = "parameter-description";

		[Fact]
		public void AddMultiple_When_called_with_valid_configuration_Then_collects_profiles_from_calling_assembly()
		{
			var configurationRegistry = GetConfigurationRegistry();
			configurationRegistry.AddMultiple(new[] { new StubCommandConfiguration() });

			var existingConfigurations = configurationRegistry.GetAll();

			Assert.Equal(1, existingConfigurations.Count());
		}

		[Fact]
		public void GetAll_When_called_when_no_configurations_were_registered_Then_returns_empty_collection()
		{
			var configurationRegistry = GetConfigurationRegistry();

			var existingConfigurations = configurationRegistry.GetAll();

			Assert.Empty(existingConfigurations);
		}

		[Fact]
		public void GetAll_When_called_when_configuration_was_registered_Then_returns_currently_registered_configuration()
		{
			var configurationRegistry = GetConfigurationRegistry();
			var stubParserConfiguration = new StubCommandConfiguration();

			configurationRegistry.AddMultiple(new[] { stubParserConfiguration });

			var existingConfigurations = configurationRegistry.GetAll();

			Assert.Equal(stubParserConfiguration.Configuration, existingConfigurations.Single());
		}

		[Fact]
		public void AddMultiple_When_called_with_valid_configuration_Then_unmapped_parameters_are_not_used()
		{
			var configurationRegistry = GetConfigurationRegistry();
			configurationRegistry.AddMultiple(new[] { new StubCommandConfiguration() });

			var existingConfigurations = configurationRegistry.GetAll();
			var stubConfiguration = existingConfigurations.Single();

			Assert.Equal(1, stubConfiguration.Parameters.Count());
		}

		[Fact]
		public void AddMultiple_When_called_with_valid_configuration_Then_configuration_contains_specified_command_definition()
		{
			var configurationRegistry = GetConfigurationRegistry();
			configurationRegistry.AddMultiple(new[] { new StubCommandConfiguration() });

			var existingConfigurations = configurationRegistry.GetAll();
			var stubConfiguration = existingConfigurations.Single();
			var configurationParameter = stubConfiguration.Parameters.Single();

			Assert.Equal(typeof(StubCommandArguments), stubConfiguration.Type);
			Assert.Equal(CommandSignature, stubConfiguration.Signature);
			Assert.Equal(CommandDescription, stubConfiguration.Description);
			Assert.Equal(ParameterName, configurationParameter.Signature);
			Assert.Equal(ParameterDescription, configurationParameter.Description);
		}

		[Fact]
		public void AddMultiple_When_called_with_duplicate_configurations_Then_throws()
		{
			var configurationRegistry = GetConfigurationRegistry();

			Assert.Throws<ArgumentException>(
				() =>
					configurationRegistry.AddMultiple(new ICommandConfiguration[]
					                                  {
						                                  new StubCommandConfiguration(),
						                                  new DuplicateSignatureCommandConfiguration()
					                                  }));
		}

		[Fact]
		public void AddMultiple_When_conscequently_called_with_duplicate_configurations_Then_throws()
		{
			var configurationRegistry = GetConfigurationRegistry();

			configurationRegistry.AddMultiple(new[] { new StubCommandConfiguration() });
			Assert.Throws<ArgumentException>(
				() => configurationRegistry.AddMultiple(new[] { new DuplicateSignatureCommandConfiguration() }));
		}

		public class StubCommandArguments : IArguments
		{
			public string Name { get; set; }

			public string Description { get; set; }
		}

		public class StubCommandConfiguration : CommandConfiguration<StubCommandArguments>
		{
			public StubCommandConfiguration()
			{
				HasSignature(CommandSignature);

				HasDescription(CommandDescription);

				HasArgument(x => x.Name)
					.WithSignature(ParameterName)
					.WithDescription(ParameterDescription);
			}
		}

		public class DuplicateSignatureCommandConfiguration : CommandConfiguration<StubCommandArguments>
		{
			public DuplicateSignatureCommandConfiguration()
			{
				HasSignature(CommandSignature);
			}
		}

		private ICommandConfigurationRegistry GetConfigurationRegistry()
		{
			return new CommandConfigurationRegistry();
		}
	}
}