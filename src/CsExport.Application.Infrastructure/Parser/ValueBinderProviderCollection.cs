using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace CsExport.Application.Infrastructure.Parser
{
	public class ValueBinderProviderCollection : IValueBinderConfiguration
	{
		private readonly IDictionary<Type, IPropertyValueBinderProvider> _valueBinderProviders =
			new ConcurrentDictionary<Type, IPropertyValueBinderProvider>();

		public bool ContainsValueForType(Type type)
		{
			return _valueBinderProviders.ContainsKey(type);
		}

		public IPropertyValueBinderProvider GetForType(Type propertyType)
		{
			return _valueBinderProviders.ContainsKey(propertyType)
				? _valueBinderProviders[propertyType]
				: null;
		}

		public void AddOrUpdate<TRecord>(Func<object, PropertyInfo, IReflectionPropertyValueBinder> value)
		{
			_valueBinderProviders[typeof(TRecord)] = new PropertyValueBinderProvider(value);
		}

		public void Remove<TRecord>()
		{
			if (_valueBinderProviders.ContainsKey(typeof(TRecord)) == false)
				throw new InvalidOperationException("Specified type does not exist");

			_valueBinderProviders.Remove(typeof(TRecord));
		}
	}
}