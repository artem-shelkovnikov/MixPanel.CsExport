using System.Reflection;

namespace CsExport.Application.Infrastructure.Parser.Utility
{
	public interface IReflectionPropertyBinderFactory
	{
		IReflectionPropertyValueBinder CreateForProperty(object o, PropertyInfo propertyInfo);
	}
}