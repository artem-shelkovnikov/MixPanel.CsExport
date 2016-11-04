using System.Reflection;

namespace CsExport.Application.Infrastructure.Parser
{
	public interface IPropertyValueBinderProvider
	{
		IReflectionPropertyValueBinder Provide(object o, PropertyInfo propertyInfo);
	}
}