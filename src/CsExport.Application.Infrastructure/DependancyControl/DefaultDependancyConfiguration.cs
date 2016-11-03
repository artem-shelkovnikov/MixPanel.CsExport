using CsExport.Application.Infrastructure.IO;

namespace CsExport.Application.Infrastructure.DependancyControl
{
	internal class DefaultDependancyConfiguration : DependancyConfiguration
	{
		protected override void Register(IDependancyContainer dependancyContainer)
		{
			dependancyContainer.Register<IOutput, ConsoleOutput>();
		}
	}
}