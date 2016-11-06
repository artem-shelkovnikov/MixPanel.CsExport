using CsExport.Application.Infrastructure.DependancyControl;
using CsExport.Application.Infrastructure.FluentConfiguration;

namespace CsExport.Application.Infrastructure
{
	public interface IConsoleApplicationFactory
	{
		IConsoleApplication Create(ICommandConfigurationRegistry commandConfigurationRegistry,
		                           IDependancyContainer dependancyContainer,
		                           ApplicationConfiguration applicationConfiguration);
	}
}