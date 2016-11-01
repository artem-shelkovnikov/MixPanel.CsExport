using CsExport.Application.Logic.DependancyControl;
using CsExport.Application.Logic.Parser;

namespace CsExport.Application.Logic
{
	public interface IConsoleApplicationFactory
	{
		IConsoleApplication Create(ICommandConfigurationRegistry commandConfigurationRegistry,
		                           IDependancyContainer dependancyContainer);
	}
}