using System.Reflection;
using CsExport.Application.Infrastructure.Parser.Utility.ValueBinders;
using CsExport.Core;

namespace CsExport.Application.Infrastructure.Parser.Utility
{
	public class ReflectionPropertyBinderFactory : IReflectionPropertyBinderFactory
	{
		public IReflectionPropertyValueBinder CreateForProperty(object o, PropertyInfo propertyInfo)
		{
			if (propertyInfo.PropertyType == typeof(string))
				return new StringValueBinder(o, propertyInfo);

			if (propertyInfo.PropertyType == typeof(string[]))
				return new StringArrayValueBinder(o, propertyInfo);

			if (propertyInfo.PropertyType == typeof(Date))
				return new DateValueBinder(o, propertyInfo);

			return null;
		}
	}
}