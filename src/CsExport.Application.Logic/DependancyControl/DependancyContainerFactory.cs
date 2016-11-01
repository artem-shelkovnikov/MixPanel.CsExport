using Autofac;

namespace CsExport.Application.Logic.DependancyControl
{
	internal class DependancyContainerFactory : IDependancyContainerFactory
	{
		public IDependancyContainer Create()
		{
			return new DependancyContainer(new ContainerBuilder().Build());
		}
	}
}