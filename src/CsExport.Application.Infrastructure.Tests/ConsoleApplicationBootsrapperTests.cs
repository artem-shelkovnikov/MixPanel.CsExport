using CsExport.Application.Infrastructure.DependancyControl;
using CsExport.Application.Infrastructure.FluentConfiguration;
using Moq;
using Xunit;

namespace CsExport.Application.Infrastructure.Tests
{
	public class ConsoleApplicationBootsrapperTests
	{
		private readonly ConsoleApplicationBootstrapper _consoleApplicationBootstrapper;

		private readonly Mock<IDependancyContainerFactory> _dependancyContainerFactoryMock =
			new Mock<IDependancyContainerFactory>();

		private readonly Mock<IDependancyContainer> _dependancyContainerMock = new Mock<IDependancyContainer>();

		private readonly Mock<ICommandConfigurationRegistry> _commandConfigurationRegistryMock =
			new Mock<ICommandConfigurationRegistry>();

		private readonly Mock<IConsoleApplicationFactory> _consoleApplicationFactoryMock =
			new Mock<IConsoleApplicationFactory>();

		private readonly Mock<IConsoleApplication> _consoleApplicationMock = new Mock<IConsoleApplication>();

		private readonly StubDependancyConfiguration _stubDependancyConfiguration = new StubDependancyConfiguration();

		private readonly Mock<ICommandConfiguration> _commandConfigurationMock = new Mock<ICommandConfiguration>();

		private readonly ApplicationConfiguration _applicationConfiguration = new ApplicationConfiguration();

		public ConsoleApplicationBootsrapperTests()
		{
			CommandRegistration stubCommandRegistration = new StubCommandRegistration(_commandConfigurationMock.Object);

			_consoleApplicationBootstrapper = new ConsoleApplicationBootstrapper(_dependancyContainerFactoryMock.Object,
			                                                                     _commandConfigurationRegistryMock.Object,
			                                                                     _consoleApplicationFactoryMock.Object,
			                                                                     _stubDependancyConfiguration,
			                                                                     stubCommandRegistration,
			                                                                     _applicationConfiguration);

			_dependancyContainerFactoryMock.Setup(x => x.Create()).Returns(_dependancyContainerMock.Object);
			_consoleApplicationFactoryMock.Setup(
				                              x =>
					                              x.Create(_commandConfigurationRegistryMock.Object,
					                                       _dependancyContainerMock.Object,
					                                       _applicationConfiguration))
			                              .Returns(_consoleApplicationMock.Object);

			_consoleApplicationMock.Setup(x => x.IsTerminated()).Returns(true);
		}

		[Fact]
		public void Run_When_called_Then_creates_dependancy_container_using_factory()
		{
			_consoleApplicationBootstrapper.Run();

			_dependancyContainerFactoryMock.Verify(x => x.Create(), Times.Once);
		}

		[Fact]
		public void Run_When_called_Then_calls_defaultDependancyConfiguration_register_once()
		{
			_consoleApplicationBootstrapper.Run();

			Assert.Equal(1, _stubDependancyConfiguration.TimesCalled);
			Assert.Equal(_dependancyContainerMock.Object, _stubDependancyConfiguration.LastCaller);
		}

		//TODO: questionable logic, must refactor
		[Fact]
		public void Run_When_called_Then_registers_commandConfigurationRegistry_in_dependancyController()
		{
			_consoleApplicationBootstrapper.Run();

			_dependancyContainerMock.Verify(x => x.RegisterInstance(_commandConfigurationRegistryMock.Object));
		}

		[Fact]
		public void Run_When_called_Then_registers_default_commandRegistration_using_commandConfigurationRegistry()
		{
			_consoleApplicationBootstrapper.Run();

			_commandConfigurationRegistryMock.Verify(
				x => x.AddMultiple(It.Is<ICommandConfiguration[]>(y => y.Length == 1 && y[0] == _commandConfigurationMock.Object)));
		}

		[Fact]
		public void Run_When_called_Then_uses_applicationFactory_to_create_consoleApplication()
		{
			_consoleApplicationBootstrapper.Run();

			_consoleApplicationFactoryMock.Verify(
				x => x.Create(_commandConfigurationRegistryMock.Object, _dependancyContainerMock.Object, _applicationConfiguration),
				Times.Once);
		}

		[Fact]
		public void Run_When_called_for_application_that_terminates_after_first_call_Then_calls_application_readCommand_once()
		{
			_consoleApplicationMock.Setup(x => x.IsTerminated())
			                       .Returns(false)
			                       .Callback(() => _consoleApplicationMock.Setup(x => x.IsTerminated()).Returns(true));

			_consoleApplicationBootstrapper.Run();

			_consoleApplicationMock.Verify(x => x.ReadCommand(), Times.Once);
		}

		[Fact]
		public void Run_When_dependancy_configuration_was_added_previously_Then_registers_this_dependancy_configuration()
		{
			var anotherDependancyConfiguration = new StubDependancyConfiguration();
			_consoleApplicationBootstrapper.ConfigureDependancies(anotherDependancyConfiguration);

			_consoleApplicationBootstrapper.Run();

			Assert.Equal(1, anotherDependancyConfiguration.TimesCalled);
			Assert.Equal(_dependancyContainerMock.Object, anotherDependancyConfiguration.LastCaller);
		}

		[Fact]
		public void Run_When_command_registration_was_added_previously_Then_registers_this_command_registration()
		{
			var commandConfigurationMock = new Mock<ICommandConfiguration>();
			var newCommandConfiguration = new StubCommandRegistration(commandConfigurationMock.Object);
			_consoleApplicationBootstrapper.RegisterCommands(newCommandConfiguration);

			_consoleApplicationBootstrapper.Run();

			_commandConfigurationRegistryMock.Verify(
				x => x.AddMultiple(It.Is<ICommandConfiguration[]>(y => y.Length == 1 && y[0] == commandConfigurationMock.Object)));
		}

		private class StubDependancyConfiguration : DependancyConfiguration
		{
			public int TimesCalled { get; private set; } = 0;
			public IDependancyContainer LastCaller { get; private set; }

			protected override void Register(IDependancyContainer dependancyContainer)
			{
				TimesCalled += 1;
				LastCaller = dependancyContainer;
			}
		}

		private class StubCommandRegistration : CommandRegistration
		{
			private readonly ICommandConfiguration _commandConfiguration;

			public StubCommandRegistration(ICommandConfiguration commandConfiguration)
			{
				_commandConfiguration = commandConfiguration;
			}

			public int TimesCalled { get; private set; } = 0;

			protected override ICommandConfiguration[] Load()
			{
				TimesCalled += 1;
				return new[] { _commandConfiguration };
			}
		}
	}
}