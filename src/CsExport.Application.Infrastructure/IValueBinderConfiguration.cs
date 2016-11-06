using System;
using System.Reflection;
using CsExport.Application.Infrastructure.Parser;

namespace CsExport.Application.Infrastructure
{
	public interface IValueBinderConfiguration
	{
		void AddOrUpdate<TRecord>(Func<object, PropertyInfo, IReflectionPropertyValueBinder> value);
		void Remove<TRecord>();
	}
}