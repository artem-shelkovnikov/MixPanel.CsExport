using Autofac;

namespace CsExport.Application.Infrastructure.DependancyControl
{
	internal class DependancyContainerFactory : IDependancyContainerFactory
	{
		public IDependancyContainer Create()
		{
			return new DependancyContainer(new ContainerBuilder().Build());
		}
	}
}