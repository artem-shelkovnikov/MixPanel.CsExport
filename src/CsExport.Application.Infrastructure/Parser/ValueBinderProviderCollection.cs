using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace CsExport.Application.Infrastructure.Parser
{
	public class ValueBinderProviderCollection : IEnumerable<KeyValuePair<Type, IPropertyValueBinderProvider>>
	{
		private readonly IDictionary<Type, IPropertyValueBinderProvider> _valueBinderProviders = new ConcurrentDictionary<Type, IPropertyValueBinderProvider>();

		public void Add<T>(Func<object, PropertyInfo, IReflectionPropertyValueBinder> factory)
		{
			if (_valueBinderProviders.ContainsKey(typeof(T)))
				throw new InvalidOperationException("Value binder for that type already exists. Use Replace method instead.");

			_valueBinderProviders[typeof(T)] = new PropertyValueBinderProvider(factory);
		}

		public void Replace<T>(Func<object, PropertyInfo, IReflectionPropertyValueBinder> factory)
			where T : IReflectionPropertyValueBinder
		{
			if (_valueBinderProviders.ContainsKey(typeof(T)) == false)
				throw new InvalidOperationException("Value binder for that type already exists");

			_valueBinderProviders[typeof(T)] = new PropertyValueBinderProvider(factory);
		}	 

		public bool ContainsValueBinderProviderForType(Type type)
		{
			return _valueBinderProviders.ContainsKey(type);
		}

		public IEnumerator<KeyValuePair<Type, IPropertyValueBinderProvider>> GetEnumerator()
		{
			return _valueBinderProviders.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IPropertyValueBinderProvider this[Type propertyType]
		{
			get
			{
				return _valueBinderProviders.ContainsKey(propertyType)
					? _valueBinderProviders[propertyType]
					: null;
			}
		}
	}
}