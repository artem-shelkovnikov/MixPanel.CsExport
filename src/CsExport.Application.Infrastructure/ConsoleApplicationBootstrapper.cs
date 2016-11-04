using System.Collections.Generic;
using System.Collections.ObjectModel;
using CsExport.Application.Infrastructure.DependancyControl;
using CsExport.Application.Infrastructure.FluentConfiguration;
using CsExport.Application.Infrastructure.Parser;

namespace CsExport.Application.Infrastructure
{
	public class ConsoleApplicationBootstrapper
	{
		private readonly IDependancyContainerFactory _dependancyContainerFactory;
		private readonly ICommandConfigurationRegistry _commandConfigurationRegistry;
		private readonly IConsoleApplicationFactory _consoleApplicationFactory;

		private readonly ICollection<CommandRegistration> _userDefinedCommandRegistrations =
			new Collection<CommandRegistration>();

		private readonly ICollection<DependancyConfiguration> _userDefinedDependancyConfigurations =
			new Collection<DependancyConfiguration>();

		private readonly ApplicationConfiguration _applicationConfiguration;

		public ConsoleApplicationBootstrapper()
			: this(
				new DependancyContainerFactory(),
				new CommandConfigurationRegistry(),
				new ConsoleApplicationFactory(),
				new DefaultDependancyConfiguration(),
				new DefaultCommandRegistration(),
				new ApplicationConfiguration())
		{
		}

		internal ConsoleApplicationBootstrapper(IDependancyContainerFactory dependancyContainerFactory,
		                                        ICommandConfigurationRegistry commandConfigurationRegistry,
		                                        IConsoleApplicationFactory consoleApplicationFactory,
		                                        DependancyConfiguration defaultDependancyConfiguration,
		                                        CommandRegistration defaultCommandRegistration,
		                                        ApplicationConfiguration applicationConfiguration)
		{
			_dependancyContainerFactory = dependancyContainerFactory;
			_commandConfigurationRegistry = commandConfigurationRegistry;
			_consoleApplicationFactory = consoleApplicationFactory;
			_userDefinedDependancyConfigurations.Add(defaultDependancyConfiguration);
			_userDefinedCommandRegistrations.Add(defaultCommandRegistration);
			_applicationConfiguration = applicationConfiguration;
		}

		public ValueBinderProviderCollection ValueBinders => _applicationConfiguration.ValueBinderProviderCollection;

		public void Run()
		{
			var dependancyContainer = _dependancyContainerFactory.Create();

			RegisterDependancies(dependancyContainer);
			RegisterConfigurationRegistryDependancy(dependancyContainer);
			RegisterCommandConfigurations(_commandConfigurationRegistry);

			var application = _consoleApplicationFactory.Create(_commandConfigurationRegistry, dependancyContainer, _applicationConfiguration);

			while (application.IsTerminated() == false)
			{
				application.ReadCommand();
			}
		}

		private void RegisterConfigurationRegistryDependancy(IDependancyContainer dependancyContainer)
		{
			dependancyContainer.RegisterInstance(_commandConfigurationRegistry);
		}

		private void RegisterCommandConfigurations(ICommandConfigurationRegistry commandConfigurationRegistry)
		{
			foreach (var commandRegistration in _userDefinedCommandRegistrations)
			{
				commandRegistration.Load(commandConfigurationRegistry);
			}
		}

		private void RegisterDependancies(IDependancyContainer dependancyContainer)
		{
			foreach (var dependancyInjectionConfiguration in _userDefinedDependancyConfigurations)
			{
				dependancyInjectionConfiguration.RegisterInternal(dependancyContainer);
			}
		}

		public ConsoleApplicationBootstrapper RegisterCommands(CommandRegistration commandRegistration)
		{
			_userDefinedCommandRegistrations.Add(commandRegistration);

			return this;
		}

		public ConsoleApplicationBootstrapper ConfigureDependancies(DependancyConfiguration dependancyConfiguration)
		{
			_userDefinedDependancyConfigurations.Add(dependancyConfiguration);

			return this;
		}
	}
}