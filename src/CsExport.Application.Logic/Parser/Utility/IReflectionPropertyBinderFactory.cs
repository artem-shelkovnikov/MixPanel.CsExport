using System.Reflection;

namespace CsExport.Application.Logic.Parser.Utility
{
	internal interface IReflectionPropertyBinderFactory
	{
		IReflectionPropertyValueBinder CreateForProperty(object o, PropertyInfo propertyInfo);
	}
}