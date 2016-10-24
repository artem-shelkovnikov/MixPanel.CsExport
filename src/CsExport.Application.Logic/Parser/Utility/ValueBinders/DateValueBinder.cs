using System;
using System.Reflection;
using CsExport.Core;

namespace CsExport.Application.Logic.Parser.Utility.ValueBinders
{
	public class DateValueBinder : ReflectionPropertyValueBinderBase<Date>
	{
		public DateValueBinder(object @object, PropertyInfo propertyInfo) : base(@object, propertyInfo)
		{
		}

		protected override Date ParseValue(string value)
		{
			DateTime dateTime;
			if (DateTime.TryParse(value, out dateTime) == false)
				throw new FormatException();

			return new Date(dateTime);
		}
	}
}