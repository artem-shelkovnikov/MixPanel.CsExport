using System;

namespace CsExport.Application.Infrastructure.DependancyControl
{
	public interface IDependancyContainer
	{
		T Resolve<T>();
		object Resolve(Type t);
		void Register<TDeclaration, TImlpementation>();

		void RegisterInstance<T>(T instance) where T : class;
	}
}