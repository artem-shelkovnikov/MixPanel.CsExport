using System.Reflection;

namespace CsExport.Application.Logic.Parser.Utility
{
	public interface IReflectionPropertyBinderFactory
	{
		IReflectionPropertyValueBinder CreateForProperty(object o, PropertyInfo propertyInfo);
	}
}