using System;

namespace CsExport.Application.Logic
{
	public interface IDependancyInjectionService
	{
		T Resolve<T>();
		object Resolve(Type t);
	}
}