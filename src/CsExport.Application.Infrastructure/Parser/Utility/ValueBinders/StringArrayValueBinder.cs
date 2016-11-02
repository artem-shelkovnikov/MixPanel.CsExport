using System.Linq;
using System.Reflection;

namespace CsExport.Application.Infrastructure.Parser.Utility.ValueBinders
{
	public class StringArrayValueBinder : ReflectionPropertyValueBinderBase<string[]>
	{
		public StringArrayValueBinder(object @object, PropertyInfo propertyInfo) : base(@object, propertyInfo)
		{
		}

		protected override string[] ParseValue(string value)
		{
			return value.Split(';').Select(x => x.Trim()).ToArray();
		}
	}
}