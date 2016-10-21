using System;
using System.Reflection;

namespace CsExport.Application.Logic.Parser.Utility
{
	public abstract class ReflectionPropertyValueBinderBase<TPropertyValue> : IReflectionPropertyValueBinder
	{
		private readonly object _o;
		private readonly PropertyInfo _propertyInfo;

		protected object Source { get { return _o; } }
		protected PropertyInfo PropertyInfo { get { return _propertyInfo; } }

		protected ReflectionPropertyValueBinderBase(object o, PropertyInfo propertyInfo)
		{
			_o = o;
			_propertyInfo = propertyInfo;
		}

		public void BindValue(string value)
		{
			try
			{
				TPropertyValue propertyValue;
				if (string.IsNullOrEmpty(value))
					propertyValue = default(TPropertyValue);
				else
					propertyValue = ParseValue(value);

				_propertyInfo.SetValue(_o, propertyValue);
			}
			catch (Exception ex)
			{
				throw new ParameterBindingException(ex);
			}
			
		}

		protected abstract TPropertyValue ParseValue(string value);
	}
}