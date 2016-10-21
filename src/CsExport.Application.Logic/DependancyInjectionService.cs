using System;
using Autofac;

namespace CsExport.Application.Logic
{
	public class DependancyInjectionService : IDependancyInjectionService
	{
		private readonly IContainer _container;

		public DependancyInjectionService(IContainer container)
		{
			_container = container;
		}
			  
		public T Resolve<T>()
		{
			return _container.BeginLifetimeScope().Resolve<T>();
		}

		public object Resolve(Type t)
		{
			return _container.BeginLifetimeScope().Resolve(t);
		}
	}
}
