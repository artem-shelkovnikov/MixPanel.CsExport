using CsExport.Application.Infrastructure.DependancyControl;
using CsExport.Application.Infrastructure.Parser;

namespace CsExport.Application.Infrastructure
{
	public interface IConsoleApplicationFactory
	{
		IConsoleApplication Create(ICommandConfigurationRegistry commandConfigurationRegistry,
		                           IDependancyContainer dependancyContainer);
	}
}