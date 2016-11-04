using System.Reflection;
using CsExport.Application.Infrastructure.Parser.ValueBinders;

namespace CsExport.Application.Infrastructure.Parser
{
	public class ReflectionPropertyBinderFactory : IReflectionPropertyBinderFactory
	{
		private readonly ValueBinderProviderCollection _valueBinderProviderCollection;

		public ReflectionPropertyBinderFactory(ValueBinderProviderCollection valueBinderProviderCollection)
		{
			_valueBinderProviderCollection = valueBinderProviderCollection;		  
			_valueBinderProviderCollection.Add<string>((o, info) => new StringValueBinder(o, info));
			_valueBinderProviderCollection.Add<string[]>((o, info) => new StringArrayValueBinder(o, info));					   
		}

		public IReflectionPropertyValueBinder CreateForProperty(object o, PropertyInfo propertyInfo)
		{
			var propertyType = propertyInfo.PropertyType;

			if (_valueBinderProviderCollection.ContainsValueBinderProviderForType(propertyType) == false)
				return null;

			var propertyValueBinderProvider = _valueBinderProviderCollection[propertyType];

			return propertyValueBinderProvider.Provide(o, propertyInfo);	
		}
	}
}