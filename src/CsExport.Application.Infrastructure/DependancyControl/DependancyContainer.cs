using System;
using Autofac;

namespace CsExport.Application.Infrastructure.DependancyControl
{
	internal class DependancyContainer : IDependancyContainer
	{
		private readonly IContainer _container;

		public DependancyContainer(IContainer container)
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

		public void Register<TDeclaration, TImlpementation>()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<TImlpementation>().As<TDeclaration>();
			builder.Update(_container);
		}

		public void RegisterInstance<T>(T instance) where T : class
		{
			var builder = new ContainerBuilder();
			builder.RegisterInstance<T>(instance);
			builder.Update(_container);
		}
	}
}