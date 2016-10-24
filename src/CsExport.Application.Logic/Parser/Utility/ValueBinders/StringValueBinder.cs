using System.Reflection;

namespace CsExport.Application.Logic.Parser.Utility.ValueBinders
{
	public class StringValueBinder : ReflectionPropertyValueBinderBase<string>
	{
		public StringValueBinder(object @object, PropertyInfo propertyInfo) : base(@object, propertyInfo)
		{
		}

		protected override string ParseValue(string value)
		{
			return value;
		}
	}
}