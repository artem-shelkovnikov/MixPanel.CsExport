using System.Reflection;

namespace CsExport.Application.Infrastructure.Parser
{
	public interface IReflectionPropertyBinderFactory
	{
		IReflectionPropertyValueBinder CreateForProperty(object o, PropertyInfo propertyInfo);
	}
}