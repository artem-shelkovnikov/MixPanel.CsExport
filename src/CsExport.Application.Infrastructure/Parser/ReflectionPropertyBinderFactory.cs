using System.Reflection;

namespace CsExport.Application.Infrastructure.Parser
{
	public class ReflectionPropertyBinderFactory : IReflectionPropertyBinderFactory
	{
		private readonly ValueBinderProviderCollection _valueBinderProviderCollection;

		public ReflectionPropertyBinderFactory(ValueBinderProviderCollection valueBinderProviderCollection)
		{
			_valueBinderProviderCollection = valueBinderProviderCollection;
		}

		public IReflectionPropertyValueBinder CreateForProperty(object o, PropertyInfo propertyInfo)
		{
			var propertyType = propertyInfo.PropertyType;

			if (_valueBinderProviderCollection.ContainsValueForType(propertyType) == false)
				return null;

			var propertyValueBinderProvider = _valueBinderProviderCollection.GetForType(propertyType);

			return propertyValueBinderProvider.Provide(o, propertyInfo);
		}
	}
}