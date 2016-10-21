using System;

namespace CsExport.Application.Logic.Parser.Utility
{
	public class ParameterBindingException : Exception
	{
		private readonly string _parameterName;

		public string ParameterName { get { return _parameterName; } }

		public ParameterBindingException()
		{
			
		}

		public ParameterBindingException(Exception innerException) : base("Failed to bind one of parameters", innerException)
		{
			
		}

		public ParameterBindingException(string parameterName) : base(string.Format("Failed to bind parameter: {0}", parameterName))
		{
			_parameterName = parameterName;
		}
	}
}