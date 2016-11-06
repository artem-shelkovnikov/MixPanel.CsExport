using System;
using System.Reflection;

namespace CsExport.Application.Infrastructure.Parser
{
	class PropertyValueBinderProvider : IPropertyValueBinderProvider
	{
		private readonly Func<object, PropertyInfo, IReflectionPropertyValueBinder> _factory;

		public PropertyValueBinderProvider(Func<object, PropertyInfo, IReflectionPropertyValueBinder> factory)
		{
			_factory = factory;
		}

		public IReflectionPropertyValueBinder Provide(object o, PropertyInfo propertyInfo)
		{
			return _factory(o, propertyInfo);
		}
	}
}