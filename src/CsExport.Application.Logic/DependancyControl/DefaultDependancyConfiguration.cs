using CsExport.Application.Logic.IO;

namespace CsExport.Application.Logic.DependancyControl
{
	public class DefaultDependancyConfiguration : DependancyConfiguration
	{
		protected override void Register(IDependancyContainer dependancyContainer)
		{
			dependancyContainer.Register<IOutput, ConsoleOutput>();
		}
	}
}